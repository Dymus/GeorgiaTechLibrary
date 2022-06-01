using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;

namespace GeorgiaTechLibrary.Business
{
    public class MemberManagement
    {
        private readonly IMemberRepository _memberRepository;
        public MemberManagement(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public Task<IEnumerable<Member>> GetMembers() => _memberRepository.GetMembers();

        public Task<Member> GetMember(string SSN) => _memberRepository.GetMember(SSN);

        public Task<Member> CreateMember(Member member) => _memberRepository.CreateMember(member);
    }
}
