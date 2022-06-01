using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembers();

        Task<Member> GetMember(string SSN);

        Task<Member> CreateMember(Member member);
    }
}
