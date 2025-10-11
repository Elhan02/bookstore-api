using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardsController : ControllerBase
    {
        private AwardService _awardService;

        public AwardsController(AwardService awardService)
        {
            _awardService = awardService;
        }

        // GET: api/awards
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                return Ok(await _awardService.GetAllAsync());
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
                Award award = await _awardService.GetByIdAsync(id);
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
        public async Task<IActionResult> PostAsync(Award award)
        {
            try
            {
                Award createdAward = await _awardService.CreateAsync(award);

                return Ok(createdAward);
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
                Award updatedAward = await _awardService.UpdateAsync(id, award);

                if (updatedAward == null)
                {
                    return NotFound($"Award with ID: {id} not found.");
                }

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
                bool result = await _awardService.DeleteAsync(id);

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
