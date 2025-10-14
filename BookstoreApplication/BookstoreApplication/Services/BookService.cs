using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _booksRepository;
        private readonly IAuthorRepository _authorsRepository;
        private readonly IPublisherRepository _publishersRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IPublisherRepository publisherRepository, IMapper mapper, ILogger<BookService> logger)
        {
            _booksRepository = bookRepository;
            _authorsRepository = authorRepository;
            _publishersRepository = publisherRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all books from repository.");
            List<Book> books = await _booksRepository.GetAllAsync();
            return books
                .Select(_mapper.Map<BookDto>)
                .ToList();
        }

        public async Task<BookDetailsDto> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Fetching book with ID {id} from repository.");
            Book book =  await _booksRepository.GetByIdAsync(id);

            if (book == null)
            {
                _logger.LogWarning($"Book with ID {id} was not found.");
                throw new NotFoundException("Book", id);
            }
            _logger.LogInformation($"Book with ID {id} was successfully retrieved.");

            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task<Book> CreateAsync(Book book)
        { 
            _logger.LogInformation($"Attempting to create a new book: {book.Title}, AuthorId: {book.AuthorId}, PublisherId: {book.PublisherId}");
            // kreiranje knjige je moguće ako je izabran postojeći autor i izdavac
            Author author = await _authorsRepository.GetByIdAsync(book.AuthorId);
            Publisher publisher = await _publishersRepository.GetByIdAsync(book.PublisherId);

            if (author == null)
            {
                _logger.LogWarning($"Author with ID {book.AuthorId} not found. Cannot create book.");
                throw new NotFoundException("Author", book.AuthorId);
            }
            
            if (publisher == null)
            {
                _logger.LogWarning($"Publisher with ID {book.PublisherId} not found. Cannot create book.");
                throw new NotFoundException("Publisher", book.PublisherId);
            }

            book.Author = author;
            book.Publisher = publisher;
            book.PublishedDate = DateTime.SpecifyKind(book.PublishedDate, DateTimeKind.Utc);

            Book createdBook = await _booksRepository.CreateAsync(book);

            _logger.LogInformation($"Book created successfully with ID {createdBook.Id}.");

            return createdBook;
        }

        public async Task<Book> UpdateAsync(int id, Book book)
        {
            _logger.LogInformation($"Attempting to update book with ID {id}.");
            if (id != book.Id)
            {
                _logger.LogWarning($"Book ID mismatch: route ID {id} does not match payload ID {book.Id}");
                throw new BadRequestException("Identifier value is invalid.");
            }

            Book existingBook = await _booksRepository.GetByIdAsync(id);

            if (existingBook == null)
            {
                _logger.LogWarning($"Book with ID {id} was not found.");
                throw new NotFoundException("Book", id);
            }

            // izmena knjige je moguca ako je izabran postojeći autor ili izdavac
            Author author = await _authorsRepository.GetByIdAsync(book.AuthorId);
            Publisher publisher = await _publishersRepository.GetByIdAsync(book.PublisherId);

            if (author == null)
            {
                _logger.LogWarning($"Author with ID {book.AuthorId} not found. Cannot update book.");
                throw new NotFoundException("Author", book.AuthorId);
            }

            if (publisher == null)
            {
                _logger.LogWarning($"Publisher with ID {book.PublisherId} not found. Cannot update book.");
                throw new NotFoundException("Publisher", book.PublisherId);
            }

            existingBook.Title = book.Title;
            existingBook.PageCount = book.PageCount;
            existingBook.PublishedDate = DateTime.SpecifyKind(book.PublishedDate, DateTimeKind.Utc);
            existingBook.AuthorId = book.AuthorId;
            existingBook.PublisherId = book.PublisherId;
            existingBook.ISBN = book.ISBN;
            existingBook.Author = author;
            existingBook.Publisher = publisher;
            existingBook.Id = id;

            Book updatedBook = await _booksRepository.UpdateAsync(existingBook);

            _logger.LogInformation($"Book with ID {id} updated successfully.");

            return updatedBook;
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation($"Attempting to delete book with ID {id}.");

            Book book = await _booksRepository.GetByIdAsync(id);

            if (book == null)
            {
                _logger.LogWarning($"Book with ID {id} was not found.");
                throw new NotFoundException("Book", id);
            }

            await _booksRepository.DeleteAsync(book);
        }
    }
}
