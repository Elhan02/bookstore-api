using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private AwardsRepository _awardsRepository;

        public AwardsController(AppDbContext context)
        {
            _awardsRepository = new AwardsRepository(context);
        }

        // GET: api/awards
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _awardsRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return Problem("An error occured while fetching awards.");
            }
        }

        // GET api/awards/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            try
            {
                Award award = await _awardsRepository.GetByIdAsync(id);
                if (award == null)
                {
                    return NotFound($"Award with ID: ${id} not found.");
                }
                return Ok(award);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetching Award with ID: ${id}");
            }
        }

        // POST api/awards
        [HttpPost]
        public async Task<IActionResult> PostAsync(int id, Award award)
        {
            try
            {
                Award existingAward = await _awardsRepository.GetByIdAsync(id);
                if (existingAward == null)
                {
                    return NotFound($"Award with ID: {id} not found.");
                }

                existingAward.Name = award.Name;
                existingAward.Description = award.Description;
                existingAward.StartedAt = award.StartedAt;
                existingAward.Id = id;

                Award updatedAward = await _awardsRepository.UpdateAsync(existingAward);
                return Ok(updatedAward);
            }
            catch (Exception ex)
            {
                return Problem("An error occuired while creating Award.");
            }
        }

        // PUT api/awards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Award award)
        {
            try
            {
                Award existingAward = await _awardsRepository.GetByIdAsync(id);
                if (existingAward == null)
                {
                    return NotFound($"Award with ID: {id} not found.");
                }

                existingAward.Name = award.Name;
                existingAward.Description = award.Description;
                existingAward.StartedAt = award.StartedAt;
                existingAward.Id = id;

                Award updatedAward = await _awardsRepository.UpdateAsync(existingAward);
                return Ok(updatedAward);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while updating Award with ID: {id}");
            }
        }

        // DELETE api/awards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                bool result = await _awardsRepository.DeleteAsync(id);

                if (!result)
                {
                    return NotFound($"Award with ID: {id} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while deleting Award with ID: {id}");
            }
        }
    }
}
