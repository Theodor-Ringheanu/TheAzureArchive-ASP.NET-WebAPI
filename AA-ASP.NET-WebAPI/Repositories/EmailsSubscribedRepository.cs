using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheAzureArchiveAPI.DataContext;
using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;

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

        public async Task<IEnumerable<EmailSubscribed>> GetEmailsAsync()
        {
            return await _context.EmailsSubscribed.ToListAsync();
        }

        public async Task<EmailSubscribed> GetEmailByIdAsync(Guid id)
        {
            return await _context.EmailsSubscribed.SingleOrDefaultAsync(a => a.IdEmail  == id);
        }

        public async Task AddEmailAsync(EmailSubscribed email)
        {
            email.IdEmail = Guid.NewGuid();
            _context.EmailsSubscribed.Add(email);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> EmailExistsAsync(Guid id)
        {
            return await _context.EmailsSubscribed.CountAsync(a => a.IdEmail == id) > 0;
        }

        public async Task<UpdateEmailSubscribed> UpdateEmailAsync(Guid id, UpdateEmailSubscribed email)
        {
            if(!await EmailExistsAsync(id)) { return null; }

            var emailUpdated = _mapper.Map<EmailSubscribed>(email);
            emailUpdated.IdEmail = id;
            _context.EmailsSubscribed.Update(emailUpdated);
            await _context.SaveChangesAsync();
            return email;
        }

        public async Task<bool> DeleteEmailAsync(Guid id)
        {
            EmailSubscribed email = await GetEmailByIdAsync(id);
            if (email == null) { return false; }
            _context.EmailsSubscribed.Remove(email);
            await _context.SaveChangesAsync(true);
            return true;
        }
    }
}
