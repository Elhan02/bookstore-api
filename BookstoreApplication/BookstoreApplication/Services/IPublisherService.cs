using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Services
{
    public interface IPublisherService
    {
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher> GetByIdAsync(int id);
        Task<Publisher> CreateAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(int id, Publisher publisher);
        Task DeleteAsync(int id);
        List<PublisherSortTypeOptionDto> GetAllSortTypes();
        Task<IEnumerable<Publisher>> GetSortedPublishers(int sortType);
    }
}
