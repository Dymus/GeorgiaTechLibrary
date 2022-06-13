using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;

namespace GeorgiaTechLibrary.Business
{
    public class LoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IVolumeRepository _volumeRepository;
        private readonly IMemberRepository _memberRepository;   

        public LoanService(ILoanRepository loanRepository, IVolumeRepository volumeRepository, IMemberRepository memberRepository)
        {
            _loanRepository = loanRepository;
            _volumeRepository = volumeRepository;
            _memberRepository = memberRepository;
        }
        public Task<Loan> GetLoan(string loanId) => _loanRepository.GetLoan(loanId);
        public Task<IEnumerable<Loan>> GetLoans(string SSN) => _loanRepository.GetLoans(SSN);
        public Task<IEnumerable<Loan>> GetActiveLoans(string SSN) => _loanRepository.GetActiveLoans(SSN);
        public Task<int> GetNumberOfActiveLoans(string SSN) => _loanRepository.GetNumberOfActiveLoans(SSN);
        
        public async Task<Loan> CreateLoan(LoanDTO loan)
        {
            Loan insertedLoan = new Loan();
            //find a way to substitute return null
            //check if member has more than 5 loans
            if (await _memberRepository.MemberCanLoan(loan.SSN) == false) return null;
            //check if volume is available
            if (await _volumeRepository.IsVolumeAvailable(loan.volume_id) == false) return null;
            insertedLoan = await _loanRepository.CreateLoan(loan);
            //set volume to unavailable
            await _volumeRepository.SetVolumeToUnavailable(loan.volume_id);

            insertedLoan.Volume = await _volumeRepository.GetVolume(loan.volume_id);
            insertedLoan.Member = await _memberRepository.GetMember(loan.SSN);

            return insertedLoan;
        }

    }
}
