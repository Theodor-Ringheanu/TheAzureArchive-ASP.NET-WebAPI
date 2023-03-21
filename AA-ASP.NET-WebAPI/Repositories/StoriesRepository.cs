using Microsoft.EntityFrameworkCore;
using TheAzureArchiveAPI.DataContext;
using TheAzureArchiveAPI.Models;

namespace TheAzureArchiveAPI.Repositories
{
    public class StoriesRepository : IStoriesRepository
    {
        private readonly TheAzureArchiveDataContext _context;

        public StoriesRepository( TheAzureArchiveDataContext context )
        {
            _context = context;
        }

        public async Task<IEnumerable<Story>> GetStoriesAsync()
        {
            return await _context.Stories.ToListAsync();
        }

        public async Task<Story> GetStoryByIdAsync(Guid id)
        {
            return await _context.Stories.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddStoryAsync(Story story)
        {
            story.Id = Guid.NewGuid();
            _context.Stories.Add(story);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteStoryAsync(Guid id)
        {
            Story story = await GetStoryByIdAsync(id);
            if(story == null)
            {
                return false;
            }
            _context.Stories.Remove(story);
            await _context.SaveChangesAsync(true);
            return true;
        }
    }
}
