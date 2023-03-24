using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
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

        public async Task AddEmailAsync(EmailSubscribed email)
        {
            await _repository.AddEmailAsync(email);
        }
    }
}
