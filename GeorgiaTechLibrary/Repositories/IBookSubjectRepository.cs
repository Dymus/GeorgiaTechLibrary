using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface IBookSubjectRepository
    {
        Task<int> AttachSubjectToBook(string isbn, Subject subject);
    }
}
