using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookstoreApplication.DTOs;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/books
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
                return Ok(await _bookService.GetAllAsync());
        }

        // GET api/books/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            return Ok(await _bookService.GetByIdAsync(id));
        }

        // POST api/books
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _bookService.CreateAsync(book));
        }

        // PUT api/books/5
        [Authorize(Policy = "ManageBooks")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _bookService.UpdateAsync(id, book));
        }

        // DELETE api/books/5
        [Authorize(Policy = "ManageBooks")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}
