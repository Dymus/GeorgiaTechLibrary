using GeorgiaTechLibrary.Business;
using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeorgiaTechLibrary.Controllers
{   
    public class MemberController : ControllerBase
    {

        private readonly MemberService _memberService;
        private readonly LocationService _locationService;
        private readonly LibraryService _libraryService;

        public MemberController(IMemberRepository memberRepository, ILocationRepository locationRepository, ILibraryRepository libraryRepository)
        {
            _memberService = new MemberService(memberRepository);
            _locationService = new LocationService(locationRepository);
            _libraryService = new LibraryService(libraryRepository);
        }

        [HttpGet]
        [Route("/api/[controller]/GetList")]
        [ProducesResponseType(typeof(Member[]), StatusCodes.Status200OK)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<ActionResult> GetMembers()
        {
            try
            {
                var members = await _memberService.GetMembers();
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
                var member = await _memberService.GetMember(SSN);
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
                var campusLocation = await _locationService.GetLocation(memberDTO.Campus_location_id.ToString());

                var homeLocation = await _locationService.GetLocation(memberDTO.Home_location_id.ToString());

                var library = await _libraryService.GetLibrary(memberDTO.Library_id.ToString());

                if (campusLocation!=null && homeLocation!=null && library != null) 
                    res = await _memberService.CreateMember(memberDTO);

                if (res > 0)
                {
                    newMember = await _memberService.GetMember(memberDTO.SSN);
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
