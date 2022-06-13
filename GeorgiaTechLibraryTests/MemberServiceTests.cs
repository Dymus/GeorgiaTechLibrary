using GeorgiaTechLibrary.Business;
using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;
using Moq;
using Xunit.Abstractions;

namespace GeorgiaTechLibraryTests
{
    public class MemberServiceTests
    {
        private readonly MemberService _memberService;
        private readonly LoanService _loanService;
        private readonly Mock<IMemberRepository> _memberRepoMock = new Mock<IMemberRepository>();
        private readonly Mock<ILoanRepository> _loanRepoMock = new Mock<ILoanRepository>();
        public MemberServiceTests()
        {
            _memberService = new MemberService(_memberRepoMock.Object);
        }

        [Fact]
        public async Task GetMember_ShouldReturnMember_WhenMemberExists()
        {
            //Arrange
            var SSN = "000-00-1765";

            var member = new Member
            {
                SSN = SSN,
            };

            _memberRepoMock.Setup(x => x.GetMember(SSN)).ReturnsAsync(member);
            //Act
            var result = await _memberService.GetMember(SSN);
            //Assert
            Assert.Equal(member, result);
        }

        [Fact]
        public async Task GetMember_ShouldReturnNull_WhenMemberDoesNotExist()
        {
            //Arrange
            var SSN = It.IsAny<string>();
            _memberRepoMock.Setup(x => x.GetMember(SSN)).ReturnsAsync(() => null);
            //Act
            var result = await _memberService.GetMember(SSN);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task MemberCanLoan_ShouldReturnTrue_WhenMemberHasLessThan5ActiveLoans()
        {
            //Arrange 
            var activeLoans = 0;
            var canMemberLoan = false;

            var SSN = "000-00-1765";
            //_loanRepoMock.Setup(x => x.GetNumberOfActiveLoans(SSN)).ReturnsAsync(() => 3);
            //var SSN = It.IsAny<string>();

            //Act
            activeLoans = await _loanService.GetNumberOfActiveLoans(SSN);
            if (activeLoans < 5) canMemberLoan = true;
            else canMemberLoan = false;

            var result = await _memberService.MemberCanLoan(SSN);

            //Assert
            Assert.Equal(canMemberLoan, result);
        }


    }
}