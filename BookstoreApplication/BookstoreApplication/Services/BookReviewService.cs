using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Migrations;
using BookstoreApplication.Models;
using System.Security.Claims;

namespace BookstoreApplication.Services
{
    public class BookReviewService : IBookReviewService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookReviewRepository _bookReviewRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BookReviewService(IBookRepository bookRepository, IBookReviewRepository bookReviewRepository, ILogger<BookReviewService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _bookReviewRepository = bookReviewRepository;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BookReviewDto>> GetByBookIdAsync(int bookId)
        {
            _logger.LogInformation($"Fetching reviews for book with ID {bookId} from repository.");

            Book book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                _logger.LogWarning($"Book with ID {bookId} not found. Cannot fetch reviews.");
                throw new NotFoundException("Book", bookId);
            }

            List<BookReview> bookReviews = await _bookReviewRepository.GetByBookIdAsync(bookId);
            _logger.LogInformation($"Reviews for book with ID {bookId} were successfully retrieved.");

            return _mapper.Map<List<BookReviewDto>>(bookReviews);
        }

        public async Task<BookReviewDto> CreateAsync(CreateBookReviewDto bookReview, string userId)
        {
            _logger.LogInformation($"Attempting to create a new review: Book ID: {bookReview.BookId} User ID: {userId}");
            Book book = await _bookRepository.GetByIdAsync(bookReview.BookId);
            if (book == null)
            {
                _logger.LogWarning($"Book with ID {bookReview.BookId} not found. Cannot create review.");
                throw new NotFoundException("Book", bookReview.BookId);
            }

            BookReview newReview = new BookReview()
            {
                UserId = userId,
                BookId = book.Id,
                Rate = bookReview.Rate,
                Comment = bookReview.Comment,
            };

            _unitOfWork.BeginTransactionAsync();
            try
            {
                await _bookReviewRepository.CreateAsync(newReview);

                List<BookReview> allReviews = await _bookReviewRepository.GetByBookIdAsync(book.Id);
                book.AverageRating = allReviews.Average(r => r.Rate);

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
            _logger.LogInformation($"Review created successfully with ID {newReview.Id}.");
            return _mapper.Map<BookReviewDto>(newReview);
        }
    }
}
