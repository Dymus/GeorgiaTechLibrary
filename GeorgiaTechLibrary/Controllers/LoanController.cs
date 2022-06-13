using GeorgiaTechLibrary.Business;
using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeorgiaTechLibrary.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanService _loanService;

        public LoanController(ILoanRepository loanRepository, IVolumeRepository volumeRepository, IMemberRepository memberRepository)
        {
            _loanService = new LoanService(loanRepository, volumeRepository, memberRepository);
        }

        [HttpPost]
        [Route("/api/[controller]")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<ActionResult<Loan>> CreateLoan([FromBody] LoanDTO loan)
        {
            try
            {
                var result = await _loanService.CreateLoan(loan);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
