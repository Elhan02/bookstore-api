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
            return await _publisherRepository.GetByIdAsync(id);
        }

        public async Task<Publisher> CreateAsync(Publisher publisher)
        {
            return await _publisherRepository.CreateAsync(publisher);
        }

        public async Task<Publisher> UpdateAsync(int id, Publisher publisher)
        {
            Publisher existingPublisher = await _publisherRepository.GetByIdAsync(id);

            if (existingPublisher == null) return null;

            existingPublisher.Name = publisher.Name;
            existingPublisher.Address = publisher.Address;
            existingPublisher.Website = publisher.Website;
            existingPublisher.Id = id;

            return await _publisherRepository.UpdateAsync(existingPublisher);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Publisher publisher = await _publisherRepository.GetByIdAsync(id);

            if (publisher == null)
            {
                return false;
            }

            await _publisherRepository.DeleteAsync(publisher);
            return true;
        }
    }
}
