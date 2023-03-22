using Microsoft.EntityFrameworkCore;
using TheAzureArchiveAPI.Models;
using TheAzureArchiveAPI.DataTransferObjects;

namespace TheAzureArchiveAPI.DataContext
{
    public class TheAzureArchiveDataContext : DbContext
    {
        public TheAzureArchiveDataContext(DbContextOptions<TheAzureArchiveDataContext> options) : base(options) { }

        public DbSet<Story> Stories { get; set; }
        public DbSet<GetArticle> Articles { get; set; }
    }
}
