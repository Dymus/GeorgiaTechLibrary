using GeorgiaTechLibrary.Business;
using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeorgiaTechLibrary.Controllers
{   
    public class MemberController : ControllerBase
    {

        private readonly MemberService _memberManagement;
        private readonly LocationService _locationManagement;
        private readonly LibraryService _libraryManagement;

        public MemberController(IMemberRepository memberRepository, ILocationRepository locationRepository, ILibraryRepository libraryRepository)
        {
            _memberManagement = new MemberService(memberRepository);
            _locationManagement = new LocationService(locationRepository);
            _libraryManagement = new LibraryService(libraryRepository);
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
        [ProducesResponseType(typeof(Member), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> GetMember(string SSN)
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

        [HttpPost]
        [Route("/api/[controller]")]
        [ProducesResponseType(typeof(Member), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<ActionResult<Member>> CreateMember([FromBody] MemberDTO memberDTO)
        {
            try
            {
                var res = 0;
                var newMember = new Member();
                var campusLocation = await _locationManagement.GetLocation(memberDTO.Campus_location_id.ToString());

                var homeLocation = await _locationManagement.GetLocation(memberDTO.Home_location_id.ToString());

                var library = await _libraryManagement.GetLibrary(memberDTO.Library_id.ToString());

                if (campusLocation!=null && homeLocation!=null && library != null) 
                    res = await _memberManagement.CreateMember(memberDTO);

                if (res > 0)
                {
                    newMember = await _memberManagement.GetMember(memberDTO.SSN);
                    return Created("Created new member", newMember);
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}
