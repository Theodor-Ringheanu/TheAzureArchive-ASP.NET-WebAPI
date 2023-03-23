using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;
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

        public async Task CreateArticleAsync(Article article)
        {
            await _repository.CreateArticleAsync(article);
        }
        public async Task<UpdateArticle> UpdateArticleAsync(Guid id, UpdateArticle article)
        {
            return await _repository.UpdateArticleAsync(id, article);
        }

        public async Task<PatchArticle> PartiallyUpdateArticleAsync(Guid id, PatchArticle article)
        {
            return await _repository.PartiallyUpdateArticleAsync(id, article);
        }

        public async Task<bool> DeleteArticleAsync(Guid id)
        {
            return await _repository.DeleteArticleAsync(id);
        }

    }

}
