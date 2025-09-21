using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorsRepository _authorsRepository;

        public AuthorsController(AppDbContext context)
        {
            _authorsRepository = new AuthorsRepository(context);
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _authorsRepository.GetAllAsync());
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
                Author author = await _authorsRepository.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound($"Author with ID: ${id} not found.");
                }
                return Ok(author);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetching Author with ID: ${id}");
            }
        }

        // POST api/authors
        [HttpPost]
        public async Task<IActionResult> PostAsync(Author author)
        {
            try
            {
                Author createdAuthor = await _authorsRepository.CreateAsync(author);
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
                Author existingAuthor = await _authorsRepository.GetByIdAsync(id);
                if (existingAuthor == null)
                {
                    return NotFound($"Author with ID: {id} not found.");
                }

                existingAuthor.FullName = author.FullName;
                existingAuthor.Biography = author.Biography;
                existingAuthor.DateOfBirth = author.DateOfBirth;
                existingAuthor.Id = id;

                Author updatedAuthor = await _authorsRepository.UpdateAsync(existingAuthor);
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
                bool result = await _authorsRepository.DeleteAsync(id);

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
