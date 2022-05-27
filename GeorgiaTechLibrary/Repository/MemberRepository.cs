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
            var query = "SELECT TOP (1000) * FROM Member";

            using (var connection = _context.CreateConnection())
            {
                var members = await connection.QueryAsync<Member>(query);
                return members.ToList();
            }

        }

        public async Task<Member> GetMember(string SSN)
        {
            var query = "SELECT * FROM Member WHERE SSN = @SSN";

             using (var connection = _context.CreateConnection())
            {
                var member = await connection.QuerySingleOrDefaultAsync<Member>(query, new { SSN });
                return member;
            }
        }
    }
}
