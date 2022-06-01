using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;

namespace GeorgiaTechLibrary.Business
{
    public class LoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        public Task<Loan> GetLoan(string loanId) => _loanRepository.GetLoan(loanId);
        public Task<IEnumerable<Loan>> GetLoans(string SSN) => _loanRepository.GetLoans(SSN);
        public Task<IEnumerable<Loan>> GetActiveLoans(string SSN) => _loanRepository.GetActiveLoans(SSN);
        public Task<int> GetNumberOfActiveLoans(string SSN) => _loanRepository.GetNumberOfActiveLoans(SSN);
    }
}
