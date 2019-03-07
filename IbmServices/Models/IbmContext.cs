using Microsoft.EntityFrameworkCore;

namespace IbmServices.Models
{
    public class IbmContext : DbContext
    {
        public IbmContext()
        {
        }

        public IbmContext(DbContextOptions<IbmContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("IbmDB");
            }
        }

        public DbSet<Rate> Rates { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
