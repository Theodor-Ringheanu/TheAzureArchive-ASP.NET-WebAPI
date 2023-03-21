using TheAzureArchiveAPI.Models;
using TheAzureArchiveAPI.Repositories;

namespace TheAzureArchiveAPI.Services
{
    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesRepository _repository;

        public ArticlesService(IArticlesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            return await _repository.GetArticlesAsync();
        }
        public async Task<Article> GetArticleByIdAsync(Guid id)
        {
            return await _repository.GetArticleByIdAsync(id);
        }

        public async Task AddArticleAsync(Article article)
        {
            await _repository.AddArticleAsync(article);
        }

        public async Task<bool> DeleteArticleAsync(Guid id)
        {
            return await _repository.DeleteArticleAsync(id);
        }
    }

}
