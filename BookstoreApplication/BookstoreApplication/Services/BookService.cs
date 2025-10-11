using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _booksRepository;
        private readonly IAuthorRepository _authorsRepository;
        private readonly IPublisherRepository _publishersRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IPublisherRepository publisherRepository)
        {
            _booksRepository = bookRepository;
            _authorsRepository = authorRepository;
            _publishersRepository = publisherRepository;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _booksRepository.GetAllAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _booksRepository.GetByIdAsync(id);
        }

        public async Task<Book> CreateAsync(Book book)
        { 
            // kreiranje knjige je moguće ako je izabran postojeći autor i izdavac
            Author author = await _authorsRepository.GetByIdAsync(book.AuthorId);
            Publisher publisher = await _publishersRepository.GetByIdAsync(book.PublisherId);
            if (author == null || publisher == null) return null;

            book.Author = author;
            book.Publisher = publisher;
            book.PublishedDate = DateTime.SpecifyKind(book.PublishedDate, DateTimeKind.Utc);

            return (await _booksRepository.CreateAsync(book));
        }

        public async Task<Book> UpdateAsync(int id, Book book)
        {
            Book existingBook = await _booksRepository.GetByIdAsync(id);
            if (existingBook == null) return null;

            // izmena knjige je moguca ako je izabran postojeći autor ili izdavac
            Author author = await _authorsRepository.GetByIdAsync(book.AuthorId);
            Publisher publisher = await _publishersRepository.GetByIdAsync(book.PublisherId);
            if (author == null || publisher == null) return null;

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

        public async Task<bool> DeleteAsync(int id)
        {
            Book book = await _booksRepository.GetByIdAsync(id);

            if (book == null)
            {
                return false;
            }

            await _booksRepository.DeleteAsync(book);
            return true;
        }
    }
}
