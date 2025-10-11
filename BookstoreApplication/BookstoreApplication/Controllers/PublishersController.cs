using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        // GET: api/publishers
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _publisherService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return Problem("An error occured while fetching publishers.");
            }
        }

        // GET api/publishers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            try
            {
                Publisher publisher = await _publisherService.GetByIdAsync(id);
                if (publisher == null)
                {
                    return NotFound($"Publisher with ID: ${id} not found.");
                }
                return Ok(publisher);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetching Publisher with ID: ${id}");
            }
        }

        // POST api/publishers
        [HttpPost]
        public async Task<IActionResult> PostAsync(Publisher publisher)
        {
            try
            {
                Publisher createdPublisher = await _publisherService.CreateAsync(publisher);
                return Ok(createdPublisher);
            }
            catch (Exception ex)
            {
                return Problem("An error occuired while creating Publisher.");
            }
        }

        // PUT api/publishers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Publisher publisher)
        {
            try
            {
                Publisher updatedPublisher = await _publisherService.UpdateAsync(id, publisher);
                if (updatedPublisher == null)
                {
                    return NotFound($"Publisher with ID: {id} not found.");
                }

                return Ok(updatedPublisher);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while updating Publisher with ID: {id}");
            }
        }

        // DELETE api/publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                bool result = await _publisherService.DeleteAsync(id);

                if (!result)
                {
                    return NotFound($"Publisher with ID: {id} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while deleting Publisher with ID: {id}");
            }
        }
    }
}
