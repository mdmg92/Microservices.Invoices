using Microsoft.EntityFrameworkCore;
using Pay.Models;

namespace Pay.Data
{
    public class PaymentDbContext : DbContext
    {
        public DbSet<Operation> Operations { get; set; }

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }
    }
}