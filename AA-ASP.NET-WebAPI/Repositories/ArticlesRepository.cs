using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheAzureArchiveAPI.DataContext;
using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;

namespace TheAzureArchiveAPI.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly TheAzureArchiveDataContext _context;
        private readonly IMapper _mapper;

        public ArticlesRepository(TheAzureArchiveDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            return await _context.Articles.ToListAsync();
        }
        public async Task<Article> GetArticleByIdAsync(Guid id)
        {
            return await _context.Articles.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task CreateArticleAsync(Article article)
        {
            article.Id = Guid.NewGuid();
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> ArticleExistsAsync(Guid id)
        {
            return await _context.Articles.CountAsync(a => a.Id == id) > 0;
        }

        public async Task<UpdateArticle> UpdateArticleAsync(Guid id, UpdateArticle article)
        {
            if (!await ArticleExistsAsync(id)) { return null; }

            var articleUpdated = _mapper.Map<Article>(article);
            articleUpdated.Id = id;
            _context.Articles.Update(articleUpdated);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<PatchArticle> PartiallyUpdateArticleAsync(Guid id, PatchArticle article)
        {
            var articleFromDb = await GetArticleByIdAsync(id);

            if (articleFromDb == null) { return null; }
            if (!string.IsNullOrEmpty(article.Title) && article.Title != articleFromDb.Title)
            {
                articleFromDb.Title = article.Title;
            }
            if (!string.IsNullOrEmpty(article.Author) && article.Author != articleFromDb.Author)
            {
                articleFromDb.Author = article.Author;
            }
            if (article.PublicationDate.HasValue && article.PublicationDate != articleFromDb.PublicationDate)
            {
                articleFromDb.PublicationDate = article.PublicationDate.Value;
            }
            if (!string.IsNullOrEmpty(article.ImageUrl) && article.ImageUrl != articleFromDb.ImageUrl)
            {
                articleFromDb.ImageUrl = article.ImageUrl;
            }
            if (!string.IsNullOrEmpty(article.Content) && article.Content != articleFromDb.Content)
            {
                articleFromDb.Content = article.Content;
            }
            _context.Articles.Update(articleFromDb);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<bool> DeleteArticleAsync(Guid id)
        {
            Article article = await GetArticleByIdAsync(id);
            if (article == null) { return false; }
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync(true);
            return true;
        }
    }
}
