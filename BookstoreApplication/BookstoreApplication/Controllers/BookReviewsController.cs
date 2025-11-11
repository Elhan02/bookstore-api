using BookstoreApplication.DTOs;
using BookstoreApplication.Models;
using BookstoreApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookReviewsController : ControllerBase
    {
        private readonly IBookReviewService _bookReviewService;

        public BookReviewsController(IBookReviewService bookReviewService)
        {
            _bookReviewService = bookReviewService;
        }

        [Authorize]
        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetByBookIdAsync(int bookId)
        {
            return Ok(await _bookReviewService.GetByBookIdAsync(bookId));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateBookReviewDto bookReview)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(await _bookReviewService.CreateAsync(bookReview, userId));
        }
    }
}
