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

        public async Task<Loan> CreateLoan(string SSN, string ISBN)
        {
            var query = "EXEC CreateLoan @SSN', @ISBN";
            using (var connection = _context.CreateConnection())
            {
                var results = await connection.QuerySingleAsync(query, new { SSN, ISBN });
                return results;
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
