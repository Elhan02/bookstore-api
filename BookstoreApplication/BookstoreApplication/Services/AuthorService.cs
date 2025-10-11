using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorsRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorsRepository = authorRepository;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _authorsRepository.GetAllAsync();
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _authorsRepository.GetByIdAsync(id);
        }

        public async Task<Author> CreateAsync(Author author)
        {
            return (await _authorsRepository.CreateAsync(author));
        }

        public async Task<Author> UpdateAsync(int id, Author author)
        {
            Author existingAuthor = await _authorsRepository.GetByIdAsync(id);
            if (existingAuthor == null) return null;

            existingAuthor.FullName = author.FullName;
            existingAuthor.Biography = author.Biography;
            existingAuthor.DateOfBirth = author.DateOfBirth;

            return await _authorsRepository.UpdateAsync(existingAuthor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Author author = await _authorsRepository.GetByIdAsync(id);

            if (author == null)
            {
                return false;
            }

            await _authorsRepository.DeleteAsync(author);
            return true;
        }
    }
}
