using Microsoft.EntityFrameworkCore;
using TheAzureArchiveAPI.DataTransferObjects;
using TheAzureArchiveAPI.DataTransferObjects.GetObjects;

namespace TheAzureArchiveAPI.DataContext
{
    public class TheAzureArchiveDataContext : DbContext
    {
        public TheAzureArchiveDataContext(DbContextOptions<TheAzureArchiveDataContext> options) : base(options) { }

        public DbSet<Story> Stories { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}
