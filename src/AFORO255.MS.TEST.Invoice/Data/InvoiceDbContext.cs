using Invoices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Invoices.Data
{
    public class InvoiceDbContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }

        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options)
        {
        }
    }
}