using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Utils;

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
            Author author = await _authorsRepository.GetByIdAsync(id);
            if (author == null)
            {
                throw new NotFoundException("Author", id);
            }
            return author;
        }

        public async Task<PaginatedList<Author>> GetAllPagedAsync(int page)
        {
            return await _authorsRepository.GetAllPagedAsync(page);
        }

        public async Task<Author> CreateAsync(Author author)
        {
            return (await _authorsRepository.CreateAsync(author));
        }

        public async Task<Author> UpdateAsync(int id, Author author)
        {
            if (id != author.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }

            Author existingAuthor = await _authorsRepository.GetByIdAsync(id);

            if (existingAuthor == null)
            {
                throw new NotFoundException("Author", id);
            }

            existingAuthor.FullName = author.FullName;
            existingAuthor.Biography = author.Biography;
            existingAuthor.DateOfBirth = author.DateOfBirth;

            return await _authorsRepository.UpdateAsync(existingAuthor);
        }

        public async Task DeleteAsync(int id)
        {
            Author author = await _authorsRepository.GetByIdAsync(id);

            if (author == null)
            {
                throw new NotFoundException("Author", id);
            }

            await _authorsRepository.DeleteAsync(author);
        }
    }
}
