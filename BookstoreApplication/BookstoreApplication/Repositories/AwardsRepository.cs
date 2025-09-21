using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class AwardsRepository
    {
        private AppDbContext _context;

        public AwardsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Award>> GetAll()
        {
            return await _context.Awards.ToListAsync();
        }

        public async Task<Award?> GetById(int id)
        {
            return await _context.Awards.FindAsync(id);
        }

        public async Task<Award> Create(Award award)
        {
            _context.Awards.Add(award);
            await _context.SaveChangesAsync();
            return award;
        }

        public async Task<Award> Update(Award award)
        {
            _context.Awards.Update(award);
            await _context.SaveChangesAsync();
            return award;
        }

        public async Task<bool> Delete(int id)
        {
            Award award = await _context.Awards.FindAsync(id);

            if (award == null)
            {
                return false;
            }

            _context.Awards.Remove(award);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
