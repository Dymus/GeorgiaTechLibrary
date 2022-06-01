using GeorgiaTechLibrary.Business;
using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeorgiaTechLibrary.Controllers
{
    public class LibraryController : ControllerBase
    {
        private readonly LibraryManagement _libraryManagement;

        public LibraryController(ILibraryRepository libraryRepository)
        {
            _libraryManagement = new LibraryManagement(libraryRepository);
        }

        [HttpGet]
        [Route("/api/[controller]/{libraryId}")]
        [ProducesResponseType(typeof(Library), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> GetLibrary(string libraryId)
        {
            try
            {
                var library = await _libraryManagement.GetLibrary(libraryId);
                if (library == null)
                    return NotFound();
                return Ok(library);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}
