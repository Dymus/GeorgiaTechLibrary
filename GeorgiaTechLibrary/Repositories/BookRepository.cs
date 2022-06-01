using Dapper;
using GeorgiaTechLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GeorgiaTechLibrary.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DapperContext _context;
        public BookRepository(DapperContext context)
        {
            _context = context;
        }

        public Task<Book> CreateBook(Book loan)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> GetBook(string ISBN)
        {
            var query = "SELECT * FROM book where isbn=@ISBN";
            using (var connection = _context.CreateConnection())
            {
                var book = await connection.QuerySingleOrDefaultAsync<Book>(query, new { ISBN });
                return book;
            }
        }
        //fix this one to be asynchronous
        public Book GetBookIncludeVolumes(string ISBN)
        {
            var query = "SELECT * FROM book WHERE isbn=@ISBN;" + "SELECT * FROM volume WHERE isbn=@isbn";
            using (var connection = _context.CreateConnection())
            {
                using (var results = connection.QueryMultiple(query, new { ISBN }))
                {
                    var book = results.Read<Book>().SingleOrDefault();
                    var volumes = results.Read<Volume>().ToList();

                    if (book != null && volumes != null)
                    {
                        book.Volumes = volumes;
                    }
                    return book;
                }
            }
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var query = "SELECT * FROM book";
            using (var connection = _context.CreateConnection())
            {
                var books = await connection.QueryAsync<Book>(query);
                return books.ToList();
            }
        }

    }
}
