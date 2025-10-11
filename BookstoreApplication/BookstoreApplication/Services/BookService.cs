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

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IPublisherRepository publisherRepository, IMapper mapper)
        {
            _booksRepository = bookRepository;
            _authorsRepository = authorRepository;
            _publishersRepository = publisherRepository;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            List<Book> books = await _booksRepository.GetAllAsync();
            return books
                .Select(_mapper.Map<BookDto>)
                .ToList();
        }

        public async Task<BookDetailsDto> GetByIdAsync(int id)
        {
            Book book =  await _booksRepository.GetByIdAsync(id);

            if (book == null)
            {
                throw new NotFoundException("Book", id);
            }

            return _mapper.Map<BookDetailsDto>(book);
        }

        public async Task<Book> CreateAsync(Book book)
        { 
            // kreiranje knjige je moguće ako je izabran postojeći autor i izdavac
            Author author = await _authorsRepository.GetByIdAsync(book.AuthorId);
            Publisher publisher = await _publishersRepository.GetByIdAsync(book.PublisherId);

            if (author == null)
            {
                throw new NotFoundException("Author", book.AuthorId);
            }
            
            if (publisher == null)
            {
                throw new NotFoundException("Publisher", book.PublisherId);
            }

            book.Author = author;
            book.Publisher = publisher;
            book.PublishedDate = DateTime.SpecifyKind(book.PublishedDate, DateTimeKind.Utc);

            return (await _booksRepository.CreateAsync(book));
        }

        public async Task<Book> UpdateAsync(int id, Book book)
        {
            if (id != book.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }

            Book existingBook = await _booksRepository.GetByIdAsync(id);

            if (existingBook == null)
            {
                throw new NotFoundException("Book", id);
            }

            // izmena knjige je moguca ako je izabran postojeći autor ili izdavac
            Author author = await _authorsRepository.GetByIdAsync(book.AuthorId);
            Publisher publisher = await _publishersRepository.GetByIdAsync(book.PublisherId);

            if (author == null)
            {
                throw new NotFoundException("Author", book.AuthorId);
            }

            if (publisher == null)
            {
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

            return await _booksRepository.UpdateAsync(existingBook);
        }

        public async Task DeleteAsync(int id)
        {
            Book book = await _booksRepository.GetByIdAsync(id);

            if (book == null)
            {
                throw new NotFoundException("Book", id);
            }

            await _booksRepository.DeleteAsync(book);
        }
    }
}
