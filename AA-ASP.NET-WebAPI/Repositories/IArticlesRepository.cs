using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;

namespace TheAzureArchiveAPI.Repositories
{
    public interface IArticlesRepository
    {
        public Task<IEnumerable<Article>> GetArticlesAsync();
        public Task<Article> GetArticleByIdAsync(Guid id);
        public Task CreateArticleAsync(Article article);
        public Task<UpdateArticle> UpdateArticleAsync(Guid id, UpdateArticle article);
        public Task<PatchArticle> PartiallyUpdateArticleAsync(Guid id, PatchArticle article);
        public Task<bool> DeleteArticleAsync(Guid id);
    }
}
