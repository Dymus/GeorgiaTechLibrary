using GeorgiaTechLibrary.Models;
using Dapper;

namespace GeorgiaTechLibrary.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DapperContext _context;

        public SubjectRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Subject> CreateSubject(string name)
        {
            var query = "INSERT INTO subject (name) OUTPUT inserted.subject_id, inserted.name VALUES (@name)";
            using (var connection = _context.CreateConnection())
            {
                var subject = await connection.QuerySingleAsync<Subject>(query, new { name });
                return subject;
            }
        }

        public async Task<Subject> GetSubject(string name)
        {
            var query = "SELECT TOP(1) * FROM subject WHERE name=@name";

            using (var connection = _context.CreateConnection())
            {
                var subject = await connection.QuerySingleOrDefaultAsync<Subject>(query, new { name });
                return subject;
            }
        }

    }
}
