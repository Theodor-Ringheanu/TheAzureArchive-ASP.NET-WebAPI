using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;

namespace TheAzureArchiveAPI.Repositories
{
    public interface IStoriesRepository
    {
        public Task<IEnumerable<Story>> GetStoriesAsync();
        public Task<Story> GetStoryByIdAsync(Guid id);
        public Task CreateStoryAsync(Story story);
        public Task<UpdateStory> UpdateStoryAsync(Guid id, UpdateStory story);
        public Task<PatchStory> PartiallyUpdateStoryAsync(Guid id, PatchStory story);
        public Task<bool> DeleteStoryAsync(Guid id);
    }
}
