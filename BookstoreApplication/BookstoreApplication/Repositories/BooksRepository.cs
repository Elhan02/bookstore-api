using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class BooksRepository
    {
        private AppDbContext _context;

        public BooksRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .ToListAsync();
        }

        public async Task<Book?> GetById(int id)
        {

            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book> Create(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> Update(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> Delete(int id)
        {
            Book book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
