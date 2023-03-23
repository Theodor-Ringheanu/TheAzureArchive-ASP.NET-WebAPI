using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;

namespace TheAzureArchiveAPI.Services
{
    public interface IArticlesService
    {
        public Task<IEnumerable<Article>> GetArticlesAsync();
        public Task<Article> GetArticleByIdAsync(Guid id);
        public Task CreateArticleAsync(Article story);
        public Task<UpdateArticle> UpdateArticleAsync(Guid id, UpdateArticle article);
        public Task<PatchArticle> PartiallyUpdateArticleAsync(Guid id, PatchArticle article);
        public Task<bool> DeleteArticleAsync(Guid id);
    }
}
