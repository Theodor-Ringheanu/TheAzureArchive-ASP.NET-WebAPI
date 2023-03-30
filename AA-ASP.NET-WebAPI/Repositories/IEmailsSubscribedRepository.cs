using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;

namespace TheAzureArchiveAPI.Repositories
{
    public interface IEmailsSubscribedRepository
    {
        public Task<IEnumerable<EmailSubscribed>> GetEmailsAsync();
        public Task<EmailSubscribed> GetEmailByIdAsync(Guid id);
        public Task AddEmailAsync(EmailSubscribed email);
        public Task<UpdateEmailSubscribed> UpdateEmailAsync(Guid id, UpdateEmailSubscribed email);
        public Task<bool> DeleteEmailAsync(Guid id);
    }
}
