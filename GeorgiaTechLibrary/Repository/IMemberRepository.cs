using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembers();

        Task<Member> GetMember(string SSN);
    }
}
