using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheAzureArchiveAPI.DataContext;
using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;

namespace TheAzureArchiveAPI.Repositories
{
    public class StoriesRepository : IStoriesRepository
    {
        private readonly TheAzureArchiveDataContext _context;
        private readonly IMapper _mapper;

        public StoriesRepository( TheAzureArchiveDataContext context, IMapper mapper)
        {
            _mapper = mapper;
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

        public async Task CreateStoryAsync(Story story)
        {
            story.Id = Guid.NewGuid();
            _context.Stories.Add(story);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> StoryExistsAsync(Guid id)
        {
            return await _context.Stories.CountAsync(a => a.Id == id) > 0;
        }

        public async Task<UpdateStory> UpdateStoryAsync(Guid id, UpdateStory story)
        {
            if (!await StoryExistsAsync(id)) { return null; }

            var storyUpdated = _mapper.Map<Story>(story);
            storyUpdated.Id = id;
            _context.Stories.Update(storyUpdated);
            await _context.SaveChangesAsync();
            return story;
        }

        public async Task<PatchStory> PartiallyUpdateStoryAsync(Guid id, PatchStory story)
        {
            var storyFromDb = await GetStoryByIdAsync(id);

            if (storyFromDb == null) { return null; }
            if (!string.IsNullOrEmpty(story.Title) && story.Title != storyFromDb.Title)
            {
                storyFromDb.Title = story.Title;
            }
            if (!string.IsNullOrEmpty(story.Author) && story.Author != storyFromDb.Author)
            {
                storyFromDb.Author = story.Author;
            }
            if (story.PublicationDate.HasValue && story.PublicationDate != storyFromDb.PublicationDate)
            {
                storyFromDb.PublicationDate = story.PublicationDate.Value;
            }
            if (!string.IsNullOrEmpty(story.ImageUrl) && story.ImageUrl != storyFromDb.ImageUrl)
            {
                storyFromDb.ImageUrl = story.ImageUrl;
            }
            if (!string.IsNullOrEmpty(story.Content) && story.Content != storyFromDb.Content)
            {
                storyFromDb.Content = story.Content;
            }
            _context.Stories.Update(storyFromDb);
            await _context.SaveChangesAsync();
            return story;
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
