using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBook(string ISBN);
        Task<Book> CreateBook(Book book);
        //Task<Book> GetBookIncludeVolumes(string ISBN);
    }
}
