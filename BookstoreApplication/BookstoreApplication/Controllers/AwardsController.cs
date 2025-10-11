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
        private readonly IAwardService _awardService;

        public AwardsController(IAwardService awardService)
        {
            _awardService = awardService;
        }

        // GET: api/awards
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _awardService.GetAllAsync());
        }

        // GET api/awards/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAsync(int id)
        {
            return Ok(await _awardService.GetByIdAsync(id));
        }

        // POST api/awards
        [HttpPost]
        public async Task<IActionResult> PostAsync(Award award)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _awardService.CreateAsync(award));
        }

        // PUT api/awards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Award award)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _awardService.UpdateAsync(id, award));
        }

        // DELETE api/awards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _awardService.DeleteAsync(id);
            return NoContent();
        }
    }
}
