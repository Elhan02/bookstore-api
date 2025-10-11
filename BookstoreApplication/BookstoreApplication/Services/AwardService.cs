using BookstoreApplication.Models;
using BookstoreApplication.Repositories;

namespace BookstoreApplication.Services
{
    public class AwardService
    {
        private AwardsRepository _awardsRepository;

        public AwardService(AppDbContext context)
        {
            _awardsRepository = new AwardsRepository(context);
        }

        public async Task<List<Award>> GetAllAsync()
        {
            return await _awardsRepository.GetAllAsync();
        }

        public async Task<Award> GetByIdAsync(int id)
        {
            return await _awardsRepository.GetByIdAsync(id);
        }

        public async Task<Award> CreateAsync(Award award)
        {
            return (await _awardsRepository.CreateAsync(award));
        }

        public async Task<Award> UpdateAsync(int id, Award award)
        {
            Award existingAward = await _awardsRepository.GetByIdAsync(id);
            if (existingAward == null) return null;

            existingAward.Name = award.Name;
            existingAward.Description = award.Description;
            existingAward.StartedAt = award.StartedAt;
            existingAward.Id = id;

            return await _awardsRepository.UpdateAsync(existingAward);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Award award = await _awardsRepository.GetByIdAsync(id);

            if (award == null)
            {
                return false;
            }

            await _awardsRepository.DeleteAsync(award);
            return true;
        }
    }
}
