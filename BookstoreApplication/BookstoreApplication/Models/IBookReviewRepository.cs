namespace BookstoreApplication.Models
{
    public interface IBookReviewRepository
    {
        Task<List<BookReview>> GetByBookIdAsync(int bookId);
        Task<BookReview> CreateAsync(BookReview bookReview);
    }
}
