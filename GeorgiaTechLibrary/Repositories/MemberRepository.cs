using Dapper;
using GeorgiaTechLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GeorgiaTechLibrary.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DapperContext _context;
        public MemberRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
             var query = "SELECT TOP (10) * FROM member m JOIN location l ON l.location_id=m.campus_location AND l.location_id=m.home_location JOIN library lib ON lib.library_id=m.library_id";

            using (var connection = _context.CreateConnection())
            {
                var members = await connection.QueryAsync<Member, Location, Location, Library, Member>
                    (query, map: (member, campus, home, library) =>
                {
                    member.Campus_location = campus;
                    member.Home_location = home;
                    member.Library = library;
                    return member;
                }, splitOn: "campus_location,home_location,library_id");
                return members.ToList();
            }
        }

        public async Task<Member> GetMember(string SSN)
        {
            var query = "SELECT m.ssn, m.campus_location, l1.location_id, l1.post_code, l1.street, l1.street_num, m.home_location, l2.location_id, l2.post_code, l2.street, l2.street_num, lib.library_id, lib.name FROM member m JOIN location l1 ON l1.location_id=m.campus_location JOIN location l2 ON l2.location_id=m.home_location JOIN library lib ON lib.library_id=m.library_id WHERE ssn = @SSN";

             using (var connection = _context.CreateConnection())
            {
                var member = await connection.QueryAsync<Member, Location, Location, Library, Member>(query, map: (member, campus, home, library) =>
                {
                    member.Campus_location = campus;
                    member.Home_location = home;
                    member.Library = library;
                    return member;
                },
                param: new { SSN },
                splitOn: "campus_location,home_location,library_id");
                return member.Single();
            }
        }

        public async Task<int> CreateMember(MemberDTO member)
        {
            //check if location exists; create if not (x2)
            //check if library exists; create if not

            var query = "INSERT INTO member VALUES (@SSN, @Campus_location, @Home_location, @Library_id)";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, member);
                return rowsAffected;
            }
        }

        public async Task<bool> MemberCanLoan(string SSN)
        {
            var query = "SELECT COUNT(loan_id) FROM loan l WHERE ssn=@SSN AND is_returned=0";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleAsync<int>(query, new { SSN });
                if (result < 5) return true;
                else return false;
            }
        }

        public async Task<bool> MemberCanLoanSP(string SSN)
        {
            var procedureName = "CanMemberLoan";
            var parameters = new DynamicParameters();
            parameters.Add("SSN", SSN);
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                if (result == 1) return true;
                else return false;
            }
        }
    }
}
