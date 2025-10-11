using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BookService _bookService;
        private AuthorService _authorService;
        private PublisherService _publisherService;

        public BooksController(BookService bookService, AuthorService authorService, PublisherService publisherService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _publisherService = publisherService;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _bookService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return Problem("An error occured while fetching books.");
            }
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            try
            {
                Book book = await _bookService.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound($"Book with ID: {id} not found.");
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetching Book with ID: {id}");
            }
        } 

        // POST api/books
        [HttpPost]
        public async Task<IActionResult> PostAsync(Book book)
        {
            try
            {
                Book createdBook = await _bookService.CreateAsync(book);
                
                if (createdBook == null) return NotFound($"Author or publisher not found.");

                return Ok(createdBook);
            }
            catch (Exception ex)
            {
                return Problem("An error occuired while creating Book.");
            }
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Book book)
        {
            try
            {
                if (id != book.Id) return BadRequest();

                Book updatedBook = await _bookService.UpdateAsync(id, book);

                if (updatedBook == null)
                {
                    return NotFound($"Book with ID: {id} not found.");
                }

                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while updating Book with ID: {id}");
            }
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                bool result = await _bookService.DeleteAsync(id);

                if (!result) return NotFound($"Book with ID: {id} not found");

                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while deleting Book with ID: {id}");
            }
        }
    }
}
