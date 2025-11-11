using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class BookReviewsRepository : IBookReviewRepository
    {
        private readonly AppDbContext _context;

        public BookReviewsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookReview>> GetByBookIdAsync(int bookId)
        {
           return(await _context.BookReviews.Where(b => b.BookId == bookId).ToListAsync());
        }

        public async Task<BookReview> CreateAsync(BookReview bookReview)
        {
            _context.BookReviews.Add(bookReview);
            await _context.SaveChangesAsync();
            return bookReview;
        }
    }
}
