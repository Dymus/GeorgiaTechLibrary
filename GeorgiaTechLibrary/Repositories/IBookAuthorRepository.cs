using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface IBookAuthorRepository
    {
        Task<int> AttachAuthorToBook(string isbn, Author author);
    }
}
