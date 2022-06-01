using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetMembers();

        Task<Member> GetMember(string SSN);

        Task<int> CreateMember(MemberDTO member);

        Task<bool> MemberCanLoan(string SSN);
    }
}
