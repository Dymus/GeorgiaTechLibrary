using GeorgiaTechLibrary.Business;
using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeorgiaTechLibrary.Controllers
{   
    public class MemberController : ControllerBase
    {

        private readonly MemberManagement _memberManagement;
        private readonly LocationManagement _locationManagement;
        private readonly LibraryManagement _libraryManagement;

        public MemberController(IMemberRepository memberRepository, ILocationRepository locationRepository, ILibraryRepository libraryRepository)
        {
            _memberManagement = new MemberManagement(memberRepository);
            _locationManagement = new LocationManagement(locationRepository);
            _libraryManagement = new LibraryManagement(libraryRepository);
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
        public async Task<IActionResult> CreateMember([FromBody] MemberDTO memberDTO)
        {
            try
            {
                Member member = new();
                member.SSN = memberDTO.SSN;

                var campusLocation = await _locationManagement.GetLocation(memberDTO.CampusLocation.ToString());
                if (campusLocation != null) member.CampusLocation = campusLocation;

                var homeLocation = await _locationManagement.GetLocation(memberDTO.HomeLocation.ToString());
                if (homeLocation != null) member.HomeLocation = homeLocation;

                var library = await _libraryManagement.GetLibrary(memberDTO.Library.ToString());
                if (library != null) member.Library = library;

                var newMember = await _memberManagement.CreateMember(member);
                return Created("Created new member",newMember);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}
