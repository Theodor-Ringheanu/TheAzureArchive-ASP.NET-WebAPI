using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;
using TheAzureArchiveAPI.Repositories;

namespace TheAzureArchiveAPI.Services
{
    public class EmailsSubscribedService : IEmailsSubscribedService
    {
        private readonly IEmailsSubscribedRepository _repository;

        public EmailsSubscribedService(IEmailsSubscribedRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<EmailSubscribed>> GetEmailsAsync()
        {
            return await _repository.GetEmailsAsync();
        }

        public async Task<EmailSubscribed> GetEmailByIdAsync(Guid id)
        {
            return await _repository.GetEmailByIdAsync(id);
        }

        public async Task AddEmailAsync(EmailSubscribed email)
        {
            await _repository.AddEmailAsync(email);
        }

        public async Task<UpdateEmailSubscribed> UpdateEmailAsync(Guid id, UpdateEmailSubscribed email)
        {
            return await _repository.UpdateEmailAsync(id, email);
        }

        public async Task<bool> DeleteEmailAsync(Guid id)
        {
            return await _repository.DeleteEmailAsync(id);
        }
    }
}
