using BookstoreApplication.Utils;

namespace BookstoreApplication.Models
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> CreateAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task<bool> DeleteAsync(Author author);
        Task<PaginatedList<Author>> GetAllPagedAsync(int page);
    }
}
