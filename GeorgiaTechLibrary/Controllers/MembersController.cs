using GeorgiaTechLibrary.Business;
using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeorgiaTechLibrary.Controllers
{   
    public class MembersController : ControllerBase
    {

        private readonly MemberManagement _memberManagement;

        public MembersController(IMemberRepository memberRepository)
        {
            _memberManagement = new MemberManagement(memberRepository);
        }


        [HttpGet]
        [Route("/api/[controller]/GetList")]
        [ProducesResponseType(typeof(Member[]), StatusCodes.Status200OK)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<ActionResult> GetMembers()
        {
            try
            {
                var members = await _memberManagement.GetMembers();
                return Ok(members);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/[controller]/{SSN}")]
        public async Task<IActionResult> GetCompany(string SSN)
        {
            try
            {
                var member = await _memberManagement.GetMember(SSN);
                if (member == null)
                    return NotFound();
                return Ok(member);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}
