using Dapper;
using GeorgiaTechLibrary;
using GeorgiaTechLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GeorgiaTechLibrary.Repository
{
    public class BookSubjectRepository : IBookSubjectRepository
    {
        private readonly DapperContext _context;
        public BookSubjectRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> AttachSubjectToBook(string isbn, Subject subject)
        {
            var query = "INSERT INTO book_subject (isbn, subject_id) VALUES (@isbn, @subject_id)";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { isbn, subject.Subject_id });
                return rowsAffected;
            }
        }
    }
}
