using TheAzureArchiveAPI.DataTransferObjects.GetObjects;

namespace TheAzureArchiveAPI.Services
{
    public interface IEmailsSubscribedService
    {
        public Task AddEmailAsync(EmailSubscribed email);
    }
}
