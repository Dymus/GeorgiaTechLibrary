using Dapper;
using GeorgiaTechLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GeorgiaTechLibrary.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DapperContext _context;
        public LoanRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Loan> CreateLoanSP(string SSN, string ISBN)
        {
            var query = "EXEC CreateLoan @SSN, @ISBN";
            using (var connection = _context.CreateConnection())
            {
                var results = await connection.QuerySingleAsync(query, new { SSN, ISBN });
                return results;
            }
        }

        public async Task<Loan> CreateLoan(LoanDTO loan)
        {
            var query = "INSERT INTO loan (ssn, is_returned, volume_id) OUTPUT inserted.loan_id, inserted.start_date_time, inserted.end_date_time, inserted.is_returned, inserted.volume_id VALUES (@SSN, 0, @volume_id)";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleAsync<Loan>(query, new { loan.SSN, loan.volume_id });
                return result;
            }
        }

        public async Task<Loan> GetLoan(string loanId)
        {
            var query = "SELECT * FROM loan where loan_id=@loanId";
            using (var connection = _context.CreateConnection())
            {
                var loan = await connection.QuerySingleOrDefaultAsync<Loan>(query, new { loanId });
                return loan;
            }
        }

        public async Task<IEnumerable<Loan>> GetLoans(string SSN)
        {
            var query = "SELECT * FROM loan WHERE ssn=@SSN";

            using (var connection = _context.CreateConnection())
            {
                var loans = await connection.QueryAsync<Loan>(query, new { SSN });
                return loans.ToList();
            }
        }

        public async Task<IEnumerable<Loan>> GetActiveLoans(string SSN)
        {
            var query = "SELECT * FROM loan WHERE ssn=@SSN AND is_returned=0";

            using (var connection = _context.CreateConnection())
            {
                var loans = await connection.QueryAsync<Loan>(query, new { SSN });
                return loans.ToList();
            }
        }

        public async Task<int> GetNumberOfActiveLoans(string SSN)
        {
            var query = "SELECT COUNT(loan_id) FROM loan WHERE ssn=@SSN AND is_returned=0 ";

            using (var connection = _context.CreateConnection())
            {
                var loans = await connection.QuerySingleAsync<int>(query, new { SSN });
                return loans;
            }
        }
    }
}
