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

        public async Task<IEnumerable<Book>> GetBooks()
        {
            var query = "SELECT * FROM book";
            using (var connection = _context.CreateConnection())
            {
                var books = await connection.QueryAsync<Book>(query);
                return books.ToList();
            }
        }

        public async Task<Book> CreateBook(Book book)
        {
            var query1 = "INSERT INTO book(isbn, title, description) OUTPUT inserted.isbn, inserted.title, inserted.description VALUES (@isbn, @title, @description)";
            
            using (var connection = _context.CreateConnection())
            {
                Book insertedBook = await connection.QuerySingleAsync<Book>(query1, book);
                return insertedBook;
            }
        }

        public async Task<Book> GetBook(string ISBN)
        {
            var query =
                "SELECT * FROM book WHERE isbn=@ISBN;"
                + "SELECT * FROM volume v JOIN library l on l.library_id=v.library_id WHERE isbn=@ISBN;"
                + "SELECT a.author_id, a.f_name, a.l_name FROM book b JOIN book_author ba ON ba.isbn=b.isbn JOIN author a ON a.author_id=ba.author_id WHERE b.isbn=@ISBN;"
                + "SELECT s.subject_id, s.name FROM book b JOIN book_subject sa ON sa.isbn=b.isbn JOIN subject s ON s.subject_id=sa.subject_id WHERE b.isbn=@ISBN;";
            using (var connection = _context.CreateConnection())
            {
                using (var results = await connection.QueryMultipleAsync(query, new { ISBN }))
                {
                    var book = results.Read<Book>().SingleOrDefault();
                    var volumes = results.Read<Volume>().ToList();
                    var authors = results.Read<Author>().ToList();
                    var subjects = results.Read<Subject>().ToList();

                    if (book != null && volumes != null && authors != null && subjects != null)
                    {
                        book.Volumes = volumes;
                        book.Authors = authors;
                        book.Subjects = subjects;
                    }
                    return book;
                }
            }
        }

    }
}
