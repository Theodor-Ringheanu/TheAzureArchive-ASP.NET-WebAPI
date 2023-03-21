using Microsoft.EntityFrameworkCore;
using TheAzureArchiveAPI.DataContext;
using TheAzureArchiveAPI.Models;

namespace TheAzureArchiveAPI.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly TheAzureArchiveDataContext _context;

        public ArticlesRepository(TheAzureArchiveDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            return await _context.Articles.ToListAsync();
        }
        public async Task<Article> GetArticleByIdAsync(Guid id)
        {
            return await _context.Articles.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddArticleAsync(Article article)
        {
            article.Id = Guid.NewGuid();
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteArticleAsync(Guid id)
        {
            Article article = await GetArticleByIdAsync(id);
            if (article == null)
            {
                return false;
            }
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync(true);
            return true;
        }
    }
}
