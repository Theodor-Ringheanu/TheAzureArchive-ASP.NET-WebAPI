using TheAzureArchiveAPI.DataTransferObjects;
using TheAzureArchiveAPI.DataTransferObjects.CreateUpdateObjects;
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

        public async Task<IEnumerable<GetArticle>> GetArticlesAsync()
        {
            return await _repository.GetArticlesAsync();
        }
        public async Task<GetArticle> GetArticleByIdAsync(Guid id)
        {
            return await _repository.GetArticleByIdAsync(id);
        }

        public async Task CreateArticleAsync(GetArticle article)
        {
            await _repository.CreateArticleAsync(article);
        }
        public async Task<CreateUpdateArticle> UpdateArticleAsync(Guid id, CreateUpdateArticle article)
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
