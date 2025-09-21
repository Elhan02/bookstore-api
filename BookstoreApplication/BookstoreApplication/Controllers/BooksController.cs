using BookstoreApplication.Data;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BooksRepository _booksRepository;
        private AuthorsRepository _authorsRepository;
        private PublishersRepository _publishersRepository;

        public BooksController(AppDbContext context)
        {
            _booksRepository = new BooksRepository(context);
            _authorsRepository = new AuthorsRepository(context);
            _publishersRepository = new PublishersRepository(context);
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _booksRepository.GetAll());
            }
            catch (Exception ex)
            {
                return Problem("An error occured while fetching books.");
            }
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                Book book = await _booksRepository.GetById(id);
                if (book == null)
                {
                    return NotFound($"Book with ID: ${id} not found.");
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while fetching Book with ID: ${id}");
            }
        } 

        // POST api/books
        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            try
            {
                // kreiranje knjige je moguće ako je izabran postojeći autor
                Author author = await _authorsRepository.GetById(book.AuthorId);
                if (author == null)
                {
                    return BadRequest($"Author with ID: ${book.AuthorId} not found.");
                }

                Publisher publisher = await _publishersRepository.GetById(book.PublisherId);
                if (publisher == null)
                {
                    return BadRequest($"Publisher with ID: ${book.PublisherId} not found.");
                }

                book.Author = author;
                book.Publisher = publisher;
                Book createdBook = await _booksRepository.Create(book);
                return Ok(createdBook);
            }
            catch (Exception ex)
            {
                return Problem("An error occuired while creating Book.");
            }
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Book book)
        {
            try
            {
                if (id != book.Id)
                {
                    return BadRequest();
                }

                Book existingBook = await _booksRepository.GetById(id);
                if (existingBook == null)
                {
                    return NotFound($"Book with ID: {id} not found.");
                }

                // izmena knjige je moguca ako je izabran postojeći autor
                Author author = await _authorsRepository.GetById(book.AuthorId);
                if (author == null)
                {
                    return BadRequest($"Author with ID: ${book.AuthorId} not found.");
                }

                // izmena knjige je moguca ako je izabran postojeći izdavač
                Publisher publisher = await _publishersRepository.GetById(book.PublisherId);
                if (publisher == null)
                {
                    return BadRequest($"Publisher with ID: ${book.PublisherId} not found.");
                }

                existingBook.Title = book.Title;
                existingBook.PageCount = book.PageCount;
                existingBook.PublishedDate = book.PublishedDate;
                existingBook.AuthorId = book.AuthorId;
                existingBook.PublisherId = book.PublisherId;
                existingBook.ISBN = book.ISBN;
                existingBook.Author = author;
                existingBook.Publisher = publisher;
                existingBook.Id = id;

                Book updatedBook = await _booksRepository.Update(existingBook);
                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while updating Book with ID: {id}");
            }
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _booksRepository.Delete(id);

                if (!result)
                {
                    return NotFound($"Book with ID: {id} not found");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"An error occured while deleting Book with ID: {id}");
            }
        }
    }
}
