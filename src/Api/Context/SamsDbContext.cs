using Microsoft.EntityFrameworkCore;

namespace Api.Context
{
    public class SamsDbContext : DbContext
    {
        public SamsDbContext(DbContextOptions<MvpsDashboardDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}