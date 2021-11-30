using IBM.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IBM.Infrastructure.Data
{
    public class IBMContext : DbContext
    {
        public IBMContext(DbContextOptions<IBMContext> options) : base(options)
        {

        }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
