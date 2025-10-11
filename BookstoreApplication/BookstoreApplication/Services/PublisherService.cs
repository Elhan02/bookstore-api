using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public async Task<List<Publisher>> GetAllAsync()
        {
            return await _publisherRepository.GetAllAsync();
        }

        public async Task<Publisher?> GetByIdAsync(int id)
        {
            Publisher publisher = await _publisherRepository.GetByIdAsync(id);

            if (publisher == null)
            {
                throw new NotFoundException("Publisher", id);
            }

            return publisher;
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            return await _publisherRepository.CreateAsync(publisher);
        }

        public async Task<Publisher> UpdateAsync(int id, Publisher publisher)
        {
            if (id != publisher.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }

            Publisher existingPublisher = await _publisherRepository.GetByIdAsync(id);

            if (existingPublisher == null)
            {
                throw new NotFoundException("Publisher", id);
            }

            existingPublisher.Name = publisher.Name;
            existingPublisher.Address = publisher.Address;
            existingPublisher.Website = publisher.Website;
            existingPublisher.Id = id;

            return await _publisherRepository.UpdateAsync(existingPublisher);
        }

        public async Task DeleteAsync(int id)
        {
            Publisher publisher = await _publisherRepository.GetByIdAsync(id);

            if (publisher == null)
            {
                throw new NotFoundException("Publisher", id);
            }

            await _publisherRepository.DeleteAsync(publisher);
        }
    }
}
