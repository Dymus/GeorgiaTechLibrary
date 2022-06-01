using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;

namespace GeorgiaTechLibrary.Business
{
    public class MemberService
    {
        private readonly IMemberRepository _memberRepository;
        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public Task<IEnumerable<Member>> GetMembers() => _memberRepository.GetMembers();
        public Task<Member> GetMember(string SSN) => _memberRepository.GetMember(SSN);
        public Task<int> CreateMember(MemberDTO member) => _memberRepository.CreateMember(member);
        public Task<bool> MemberCanLoan(string SSN) => _memberRepository.MemberCanLoan(SSN);

    }
}
