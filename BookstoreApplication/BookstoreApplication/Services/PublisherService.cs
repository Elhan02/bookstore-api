using BookstoreApplication.DTOs;
using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly ILogger<PublisherService> _logger;

        public PublisherService(IPublisherRepository publisherRepository, ILogger<PublisherService> logger)
        {
            _publisherRepository = publisherRepository;
            _logger = logger;
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

        public List<PublisherSortTypeOptionDto> GetAllSortTypes()
        {
            _logger.LogInformation($"Geting all publishers sort option types.");
            List<PublisherSortTypeOptionDto> publisherSortTypeOptionDtos = new List<PublisherSortTypeOptionDto>();
            var enumValues = Enum.GetValues(typeof(PublisherSortType));
            foreach (PublisherSortType enumValue in enumValues)
            {
                publisherSortTypeOptionDtos.Add(new PublisherSortTypeOptionDto(enumValue));
            }
            return publisherSortTypeOptionDtos;
        }

        public async Task<IEnumerable<Publisher>> GetSortedPublishers(int sortType)
        {
            _logger.LogInformation($"Geting all sorted publishers.");
            return await _publisherRepository.GetSortedPublishers(sortType);
        }
    }
}
