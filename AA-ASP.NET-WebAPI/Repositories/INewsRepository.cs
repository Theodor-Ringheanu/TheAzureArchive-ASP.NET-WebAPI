using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;

namespace TheAzureArchiveAPI.Repositories
{
    public interface INewsRepository
    {
        public Task<IEnumerable<News>> GetNewsAsync();
        public Task<News> GetNewsByIdAsync(Guid id);
        public Task CreateNewsAsync(News News);
        public Task<UpdateNews> UpdateNewsAsync(Guid id, UpdateNews News);
        public Task<PatchNews> PartiallyUpdateNewsAsync(Guid id, PatchNews News);
        public Task<bool> DeleteNewsAsync(Guid id);
    }
}
