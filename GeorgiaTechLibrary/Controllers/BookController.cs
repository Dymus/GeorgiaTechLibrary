using GeorgiaTechLibrary.Business;
using GeorgiaTechLibrary.Models;
using GeorgiaTechLibrary.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeorgiaTechLibrary.Controllers
{
    public class BookController : ControllerBase
    {

        private readonly BookService _bookService;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository, ISubjectRepository subjectRepository, IBookAuthorRepository bookAuthorRepository, IBookSubjectRepository bookSubjectRepository)
        {
            _bookService = new BookService(bookRepository, authorRepository, subjectRepository, bookAuthorRepository, bookSubjectRepository);
        }

        [HttpGet]
        [Route("/api/[controller]/{ISBN}")]
        [ActionName("GetByIsbn")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<IActionResult> GetByIsbn(string ISBN)
        {
            try
            {
                var book = await _bookService.GetBook(ISBN);
                if (book == null) return NotFound();
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/[controller]")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [Produces("application/json", "text/plain", "text/json")]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            try
            {
                var result = await _bookService.CreateBook(book);
                //if time make it work with CreatedAtAction
                //return CreatedAtAction("GetByIsbn", new { isbn = result.ISBN });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
