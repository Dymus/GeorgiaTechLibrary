using Dapper;
using GeorgiaTechLibrary;
using GeorgiaTechLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GeorgiaTechLibrary.Repository
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        private readonly DapperContext _context;
        public BookAuthorRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AttachAuthorToBook(string isbn, Author author)
        {
            var query = "INSERT INTO book_author (isbn, author_id) VALUES (@isbn, @author_id)";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { isbn, author.Author_id });
                return rowsAffected;
            }
        }
    }
}
