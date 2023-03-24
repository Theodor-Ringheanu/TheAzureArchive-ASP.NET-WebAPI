using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheAzureArchiveAPI.DataContext;
using TheAzureArchiveAPI.DataTransferObjects.GetObjects;

namespace TheAzureArchiveAPI.Repositories
{
    public class EmailsSubscribedRepository : IEmailsSubscribedRepository
    {
        private readonly TheAzureArchiveDataContext _context;
        private readonly IMapper _mapper;

        public EmailsSubscribedRepository(TheAzureArchiveDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddEmailAsync(EmailSubscribed email)
        {
            email.IdEmail = Guid.NewGuid();
            _context.EmailsSubscribed.Add(email);
            await _context.SaveChangesAsync();
        }
    }
}
