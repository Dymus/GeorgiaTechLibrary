using GeorgiaTechLibrary.Models;

namespace GeorgiaTechLibrary.Repository
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetLoans(string SSN);
        Task<IEnumerable<Loan>> GetActiveLoans(string SSN);
        Task<Loan> GetLoan(string loanId);
        Task<Loan> CreateLoan(LoanDTO loan);
        Task<Loan> CreateLoanSP(string SSN, string ISBN);
        Task<int> GetNumberOfActiveLoans(string SSN);

    }
}
