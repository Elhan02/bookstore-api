using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IBookReviewService
    {
        Task<List<BookReviewDto>> GetByBookIdAsync(int bookId);
        Task<BookReviewDto> CreateAsync(CreateBookReviewDto bookReview, string userId);
    }
}
