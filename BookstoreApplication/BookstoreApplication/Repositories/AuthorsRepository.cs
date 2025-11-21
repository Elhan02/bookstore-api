using BookstoreApplication.Models;
using BookstoreApplication.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class AuthorsRepository : IAuthorRepository
    {
        private AppDbContext _context;
        private const int pageSize = 5;

        public AuthorsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<PaginatedList<Author>> GetAllPagedAsync(int page)
        {
            IQueryable<Author> authors = _context.Authors
                .OrderBy(a => a.FullName);

            int pageIndex = page - 1;
            var count = await authors.CountAsync();
            var items = await authors.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            PaginatedList<Author> result = new PaginatedList<Author>(items, count, pageIndex, pageSize);
            return result;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<bool> DeleteAsync(Author author)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
