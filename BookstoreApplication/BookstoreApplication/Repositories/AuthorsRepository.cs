using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class AuthorsRepository
    {
        private AppDbContext _context;

        public AuthorsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAll()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetById(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<Author> Create(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> Update(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<bool> Delete(int id)
        {
            Author author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return false;
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
