namespace BookstoreApplication.Models
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> GetAllAsync();
        Task<Publisher?> GetByIdAsync(int id);
        Task<Publisher> CreateAsync(Publisher publisher);
        Task<Publisher> UpdateAsync(Publisher publisher);
        Task<bool> DeleteAsync(Publisher publisher);
    }
}
