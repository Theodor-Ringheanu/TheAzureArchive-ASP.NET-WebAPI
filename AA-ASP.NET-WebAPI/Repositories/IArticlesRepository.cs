using TheAzureArchiveAPI.Models;

namespace TheAzureArchiveAPI.Repositories
{
    public interface IArticlesRepository
    {
        public Task<IEnumerable<Article>> GetArticlesAsync();
        public Task<Article> GetArticleByIdAsync(Guid id);
        public Task AddArticleAsync(Article story);
        public Task<bool> DeleteArticleAsync(Guid id);
    }
}
