using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repositories
{
    public class PublishersRepository : IPublisherRepository
    {
        private AppDbContext _context;

        public PublishersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Publisher>> GetAllAsync()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            return await _context.Publishers.FindAsync(id);
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<Publisher> UpdateAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task<bool> DeleteAsync(Publisher publisher)
        {
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Publisher>> GetSortedPublishers(int sortType)
        {
            IQueryable<Publisher> publishers = _context.Publishers;

            publishers = SortPublishers(publishers, sortType);
            return await publishers.ToListAsync();
        }

        private static IQueryable<Publisher> SortPublishers(IQueryable<Publisher> publishers, int sortType)
        {
            return sortType switch
            {
                (int)PublisherSortType.NAME_ASCENDING => publishers.OrderBy(publisher => publisher.Name),
                (int)PublisherSortType.NAME_DESCENDING => publishers.OrderByDescending(publisher => publisher.Name),
                (int)PublisherSortType.ADDRESS_ASCENDING => publishers.OrderBy(publisher => publisher.Address),
                (int)PublisherSortType.ADDRESS_DESCENDING => publishers.OrderByDescending(publisher => publisher.Address),
                _ => publishers.OrderBy(publisher => publisher.Name),
            };
        }
    }
}
