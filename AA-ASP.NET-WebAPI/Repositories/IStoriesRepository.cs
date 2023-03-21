using TheAzureArchiveAPI.Models;

namespace TheAzureArchiveAPI.Repositories
{
    public interface IStoriesRepository
    {
        public Task<IEnumerable<Story>> GetStoriesAsync();
        public Task<Story> GetStoryByIdAsync(Guid id);
        public Task AddStoryAsync(Story story);
        public Task<bool> DeleteStoryAsync(Guid id);
    }
}
