using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface ISubjectRepository
    {
        Task<Subject> GetSubject(string name);
        Task<Subject> CreateSubject(string name);
    }
}
