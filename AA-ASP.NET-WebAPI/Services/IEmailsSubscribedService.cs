using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;

namespace TheAzureArchiveAPI.Services
{
    public interface IEmailsSubscribedService
    {
        public Task<IEnumerable<EmailSubscribed>> GetEmailsAsync();
        public Task<EmailSubscribed> GetEmailByIdAsync(Guid id);
        public Task AddEmailAsync(EmailSubscribed email);
        public Task<UpdateEmailSubscribed> UpdateEmailAsync(Guid id, UpdateEmailSubscribed email);
        public Task<bool> DeleteEmailAsync(Guid id);
    }
}
