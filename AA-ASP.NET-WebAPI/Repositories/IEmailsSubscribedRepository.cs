using TheAzureArchiveAPI.DataTransferObjects.GetObjects;

namespace TheAzureArchiveAPI.Repositories
{
    public interface IEmailsSubscribedRepository
    {
        public Task AddEmailAsync(EmailSubscribed email);
    }
}
