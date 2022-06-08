using GeorgiaTechLibrary.Business;
using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeorgiaTechLibrary.Controllers
{
    public class VolumeController : ControllerBase
    {
        private readonly VolumeService _volumeService;

        public VolumeController(IVolumeRepository volumeRepository)
        {
            _volumeService = new VolumeService(volumeRepository);
        }

        [HttpGet]
        [Route("/api/[controller]/GetAvailableList")]
        [ProducesResponseType(typeof(Volume[]), StatusCodes.Status200OK)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> GetAvailableVolumes(string ISBN)
        {
            try
            {
                var availableVolumes = await _volumeService.GetAvailableVolumes(ISBN);
                return Ok(availableVolumes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/[controller]/{volumeId}")]
        [ProducesResponseType(typeof(Volume), StatusCodes.Status200OK)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> GetVolume(string volumeId)
        {
            try
            {
                var availableVolumes = await _volumeService.GetVolume(volumeId);
                return Ok(availableVolumes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/[controller]/GetAvailable")]
        [ProducesResponseType(typeof(Volume), StatusCodes.Status200OK)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> GetAvailableVolume(string ISBN)
        {
            try
            {
                var availableVolumes = await _volumeService.GetAvailableVolume(ISBN);
                return Ok(availableVolumes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
