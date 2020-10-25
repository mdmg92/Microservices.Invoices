using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invoices.Models;

namespace Invoices.Data
{
    public class InvoiceDbContextSeed
    {
        public async Task SeedAsync(InvoiceDbContext context)
        {
            if (!context.Invoices.Any())
            {
                await context.Invoices.AddRangeAsync(GetPreConfiguredInvoices());
                await context.SaveChangesAsync();
            }
        }

        private IEnumerable<Invoice> GetPreConfiguredInvoices()
        {
            return new List<Invoice>
            {
                new Invoice
                {
                    Amount = 100,
                    State = 0,
                    CustomerId = 1
                },
                new Invoice
                {
                    Amount = 20,
                    State = 1,
                    CustomerId = 2
                },
                new Invoice
                {
                    Amount = 180,
                    State = 0,
                    CustomerId = 1
                },
                new Invoice
                {
                    Amount = 140,
                    State = 0,
                    CustomerId = 1
                },
                new Invoice
                {
                    Amount = 120,
                    State = 0,
                    CustomerId = 2
                },
                new Invoice
                {
                    Amount = 45,
                    State = 0,
                    CustomerId = 3
                },
                new Invoice
                {
                    Amount = 220,
                    State = 0,
                    CustomerId = 2
                },
            };
        }
    }
}