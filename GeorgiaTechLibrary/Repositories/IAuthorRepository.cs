using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface IAuthorRepository
    {
        Task<Author> GetAuthor(string fname, string lname);
        Task<Author> CreateAuthor(string fname, string lname);
    }
}
