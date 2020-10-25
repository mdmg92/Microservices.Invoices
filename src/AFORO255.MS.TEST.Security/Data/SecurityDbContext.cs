using Microsoft.EntityFrameworkCore;
using Security.Models;

namespace Security.Data
{
    public class SecurityDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
        {
        }
    }
}
