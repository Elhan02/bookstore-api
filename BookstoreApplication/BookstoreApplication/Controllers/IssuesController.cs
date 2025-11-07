using BookstoreApplication.DTOs;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;
        
        public IssuesController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [Authorize(Roles = "Editor")]
        [HttpGet("{volumeId}")]
        public async Task<IActionResult> GetIssuesByVolume(int volumeId)
        {
            return Ok(await _issueService.GetIssuesByVolume(volumeId));
        }

        [Authorize(Roles = "Editor")]
        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _issueService.CreateAsync(dto));
        }
    }
}
