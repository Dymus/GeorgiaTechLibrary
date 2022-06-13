using Dapper;
using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DapperContext _context;

        public AuthorRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Author> CreateAuthor(string fname, string lname)
        {
            var query = "INSERT INTO author (f_name, l_name) OUTPUT inserted.author_id, inserted.f_name, inserted.l_name VALUES (@fname, @lname)";
            using (var connection = _context.CreateConnection())
            {
                var author = await connection.QuerySingleAsync<Author>(query, new { fname, lname });
                return author;
            }
        } 

        public async Task<Author> GetAuthor(string fname, string lname)
        {
            var query = "SELECT TOP(1) * FROM author WHERE f_name=@fname AND l_name=@lname";

            using (var connection = _context.CreateConnection())
            {
                var author = await connection.QuerySingleOrDefaultAsync<Author>(query, new { fname, lname });
                return author;
            }
        }
    }
}
