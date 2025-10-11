using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _authorService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return Problem("An error occured while fetching authors.");
            }
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            try
            {
                Author author = await _authorService.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound($"Author with ID: {id} not found.");
                }
                return Ok(author);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetching Author with ID: {id}");
            }
        }

        // POST api/authors
        [HttpPost]
        public async Task<IActionResult> PostAsync(Author author)
        {
            try
            {
                Author createdAuthor = await _authorService.CreateAsync(author);
                return Ok(createdAuthor);
            }
            catch (Exception ex)
            {
                return Problem("An error occuired while creating Author.");
            }
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Author author)
        {
            try
            {
                Author updatedAuthor = await _authorService.UpdateAsync(id, author);
                if (updatedAuthor == null)
                {
                    return NotFound($"Author with ID: {id} not found.");
                }

                return Ok(updatedAuthor);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while updating Author with ID: {id}");
            }
        }

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletAsynce(int id)
        {
            try
            {
                bool result = await _authorService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound($"Author with ID: {id} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while deleting Author with ID: {id}");
            }
        }
    }
}
