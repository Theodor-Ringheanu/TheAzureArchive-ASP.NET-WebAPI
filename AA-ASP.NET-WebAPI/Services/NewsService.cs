using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;
using TheAzureArchiveAPI.Repositories;

namespace TheAzureArchiveAPI.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _repository;

        public NewsService(INewsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<News>> GetNewsAsync()
        {
            return await _repository.GetNewsAsync();
        }
        public async Task<News> GetNewsByIdAsync(Guid id)
        {
            return await _repository.GetNewsByIdAsync(id);
        }

        public async Task CreateNewsAsync(News news)
        {
            await _repository.CreateNewsAsync(news);
        }
        public async Task<UpdateNews> UpdateNewsAsync(Guid id, UpdateNews news)
        {
            return await _repository.UpdateNewsAsync(id, news);
        }

        public async Task<PatchNews> PartiallyUpdateNewsAsync(Guid id, PatchNews news)
        {
            return await _repository.PartiallyUpdateNewsAsync(id, news);
        }

        public async Task<bool> DeleteNewsAsync(Guid id)
        {
            return await _repository.DeleteNewsAsync(id);
        }
    }
}
