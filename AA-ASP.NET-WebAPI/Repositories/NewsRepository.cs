using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheAzureArchiveAPI.DataContext;
using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;

namespace TheAzureArchiveAPI.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly TheAzureArchiveDataContext _context;
        private readonly IMapper _mapper;

        public NewsRepository(TheAzureArchiveDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<News>> GetNewsAsync()
        {
            return await _context.News.ToListAsync();
        }
        public async Task<News> GetNewsByIdAsync(Guid id)
        {
            return await _context.News.SingleOrDefaultAsync(a => a.IdNews == id);
        }

        public async Task CreateNewsAsync(News news)
        {
            news.IdNews = Guid.NewGuid();
            _context.News.Add(news);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> NewsExistsAsync(Guid id)
        {
            return await _context.News.CountAsync(a => a.IdNews == id) > 0;
        }

        public async Task<UpdateNews> UpdateNewsAsync(Guid id, UpdateNews News)
        {
            if (!await NewsExistsAsync(id)) { return null; }

            var NewsUpdated = _mapper.Map<News>(News);
            NewsUpdated.IdNews = id;
            _context.News.Update(NewsUpdated);
            await _context.SaveChangesAsync();
            return News;
        }

        public async Task<PatchNews> PartiallyUpdateNewsAsync(Guid id, PatchNews news)
        {
            var newsFromDb = await GetNewsByIdAsync(id);

            if (newsFromDb == null) { return null; }
            if (!string.IsNullOrEmpty(news.Title) && news.Title != newsFromDb.Title)
            {
                newsFromDb.Title = news.Title;
            }
            if (!string.IsNullOrEmpty(news.Author) && news.Author != newsFromDb.Author)
            {
                newsFromDb.Author = news.Author;
            }
            if (news.PublicationDate.HasValue && news.PublicationDate != newsFromDb.PublicationDate)
            {
                newsFromDb.PublicationDate = news.PublicationDate.Value;
            }
            if (!string.IsNullOrEmpty(news.ImageUrl) && news.ImageUrl != newsFromDb.ImageUrl)
            {
                newsFromDb.ImageUrl = news.ImageUrl;
            }
            if (!string.IsNullOrEmpty(news.Content) && news.Content != newsFromDb.Content)
            {
                newsFromDb.Content = news.Content;
            }
            _context.News.Update(newsFromDb);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<bool> DeleteNewsAsync(Guid id)
        {
            News news = await GetNewsByIdAsync(id);
            if (news == null) { return false; }
            _context.News.Remove(news);
            await _context.SaveChangesAsync(true);
            return true;
        }
    }
}
