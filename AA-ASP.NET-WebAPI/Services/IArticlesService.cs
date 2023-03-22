using TheAzureArchiveAPI.DataTransferObjects;
using TheAzureArchiveAPI.DataTransferObjects.CreateUpdateObjects;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;

namespace TheAzureArchiveAPI.Services
{
    public interface IArticlesService
    {
        public Task<IEnumerable<GetArticle>> GetArticlesAsync();
        public Task<GetArticle> GetArticleByIdAsync(Guid id);
        public Task CreateArticleAsync(GetArticle story);
        public Task<CreateUpdateArticle> UpdateArticleAsync(Guid id, CreateUpdateArticle article);
        public Task<PatchArticle> PartiallyUpdateArticleAsync(Guid id, PatchArticle article);
        public Task<bool> DeleteArticleAsync(Guid id);
    }
}
