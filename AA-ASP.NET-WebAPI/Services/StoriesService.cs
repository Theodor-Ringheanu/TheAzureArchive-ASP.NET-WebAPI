using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;
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

        public async Task CreateStoryAsync(Story story)
        {
            await _repository.CreateStoryAsync(story);
        }

        public async Task<UpdateStory> UpdateStoryAsync(Guid id, UpdateStory story)
        {
            return await _repository.UpdateStoryAsync(id, story);
        }

        public async Task<PatchStory> PartiallyUpdateStoryAsync(Guid id, PatchStory story)
        {
            return await _repository.PartiallyUpdateStoryAsync(id, story);
        }

        public async Task<bool> DeleteStoryAsync(Guid id)
        {
            return await _repository.DeleteStoryAsync(id);
        }
    }
}
