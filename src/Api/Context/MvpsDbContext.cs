using Microsoft.EntityFrameworkCore;

namespace Api.Context
{
    public class MvpsDbContext : DbContext
    {
        public MvpsDbContext(DbContextOptions<MvpsDbContext> options) : base(options)
        {

        }
    }
}
