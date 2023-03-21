using TheAzureArchiveAPI.Models;

namespace TheAzureArchiveAPI.Services
{
    public interface IArticlesService
    {
        public Task<IEnumerable<Article>> GetArticlesAsync();
        public Task<Article> GetArticleByIdAsync(Guid id);
        public Task AddArticleAsync(Article story);
        public Task<bool> DeleteArticleAsync(Guid id);
    }
}
