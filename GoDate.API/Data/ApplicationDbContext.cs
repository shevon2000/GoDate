using GoDate.API.Entities.Domain;
using Microsoft.EntityFrameworkCore;

namespace GoDate.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
