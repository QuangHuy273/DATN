using Microsoft.EntityFrameworkCore;

namespace DATN.Model
{
    public class ShoeDbContext : DbContext
    {
        public ShoeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
