using GeorgiaTechLibrary.Business;
using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeorgiaTechLibrary.Controllers
{
    public class LocationController : ControllerBase
    {
        private readonly LocationManagement _locationManagement;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationManagement = new LocationManagement(locationRepository);
        }

        [HttpGet]
        [Route("/api/[controller]/{locationId}")]
        [ProducesResponseType(typeof(Location), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> GetLocation(string locationId)
        {
            try
            {
                var location = await _locationManagement.GetLocation(locationId);
                if (location == null)
                    return NotFound();
                return Ok(location);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
