using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class PublishersRepository
    {
        private AppDbContext _context;

        public PublishersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> GetAll()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher?> GetById(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        public async Task<Publisher> Create(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<Publisher> Update(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<bool> Delete(int id)
        {
            Publisher publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
            {
                return false;
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
