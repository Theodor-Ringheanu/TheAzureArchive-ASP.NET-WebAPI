using TheAzureArchiveAPI.Models;
using TheAzureArchiveAPI.Repositories;

namespace TheAzureArchiveAPI.Services
{
    public class StoriesService : IStoriesService
    {
        private readonly IStoriesRepository _repository;

        public StoriesService(IStoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Story>> GetStoriesAsync()
        {
            return await _repository.GetStoriesAsync();
        }

        public async Task<Story> GetStoryByIdAsync(Guid id)
        {
            return await _repository.GetStoryByIdAsync(id);
        }

        public async Task AddStoryAsync(Story story)
        {
            await _repository.AddStoryAsync(story);
        }

        public async Task<bool> DeleteStoryAsync(Guid id)
        {
            return await _repository.DeleteStoryAsync(id);
        }
    }
}
