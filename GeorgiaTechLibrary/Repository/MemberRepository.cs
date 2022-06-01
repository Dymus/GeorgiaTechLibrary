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
                    member.CampusLocation = campus;
                    member.HomeLocation = home;
                    member.Library = library;
                    return member;
                }, splitOn: "campus_location,home_location,library_id");
                return members.ToList();
            }
        }

        public async Task<Member> GetMember(string SSN)
        {
            var query = "SELECT * FROM member m JOIN location l ON l.location_id=m.campus_location AND l.location_id=m.home_location JOIN library lib ON lib.library_id=m.library_id WHERE ssn = @SSN";

             using (var connection = _context.CreateConnection())
            {
                var member = await connection.QueryAsync<Member, Location, Location, Library, Member>(query, map: (member, campus, home, library) =>
                {
                    member.CampusLocation = campus;
                    member.HomeLocation = home;
                    member.Library = library;
                    return member;
                },
                param: new { SSN },
                splitOn: "campus_location,home_location,library_id");
                return member.FirstOrDefault();
            }
        }

        public async Task<Member> CreateMember(Member member)
        {
            //check if location exists; create if not (x2)
            //check if library exists; create if not

            var query = "INSERT INTO member VALUES (@SSN, @campusLocation, @homeLocation, libraryId)";

            using (var connection = _context.CreateConnection())
            { 
                var rowsAffected = await connection.ExecuteAsync(query, new
                 {
                    SSN = member.SSN,
                    campusLocation = member.CampusLocation,
                    homeLocation = member.HomeLocation,
                    libraryId = member.Library,
                });
                if (rowsAffected == 0) return null;
            }
            return await GetMember(member.SSN);
        }
    }
}
