using BookstoreApplication.Exceptions;
using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class AwardService : IAwardService
    {
        private readonly IAwardRepository _awardsRepository;

        public AwardService(IAwardRepository awardRepository)
        {
            _awardsRepository = awardRepository;
        }

        public async Task<List<Award>> GetAllAsync()
        {
            return await _awardsRepository.GetAllAsync();
        }

        public async Task<Award> GetByIdAsync(int id)
        {
            Award award = await _awardsRepository.GetByIdAsync(id);

            if (award == null)
            {
                throw new NotFoundException("Award", id);    
            }

            return award;
        }

        public async Task<Award> CreateAsync(Award award)
        {
            return (await _awardsRepository.CreateAsync(award));
        }

        public async Task<Award> UpdateAsync(int id, Award award)
        {
            if (id != award.Id)
            {
                throw new BadRequestException("Identifier value is invalid.");
            }

            Award existingAward = await _awardsRepository.GetByIdAsync(id);

            if (existingAward == null)
            {
                throw new NotFoundException("Award", id);
            }

            existingAward.Name = award.Name;
            existingAward.Description = award.Description;
            existingAward.StartedAt = award.StartedAt;
            existingAward.Id = id;

            return await _awardsRepository.UpdateAsync(existingAward);
        }

        public async Task DeleteAsync(int id)
        {
            Award award = await _awardsRepository.GetByIdAsync(id);

            if (award == null)
            {
                throw new NotFoundException("Award", id);
            }

            await _awardsRepository.DeleteAsync(award);
        }
    }
}
