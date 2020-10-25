using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Security.Models;

namespace Security.Data
{
    public class SecurityDbSeed
    {
        public async Task SeedAsync(SecurityDbContext context)
        {
            if (!context.Users.Any())
            {
                await context.Users.AddRangeAsync(GetPreConfiguredInvoices());
                await context.SaveChangesAsync();
            }
        }

        private IEnumerable<User> GetPreConfiguredInvoices()
        {
            return new List<User>
            {
                new User
                {
                    Username = "mdmg",
                    Password = "a.123456"
                },
                new User
                {
                    Username = "tito",
                    Password = "mejorlateralizquierdo"
                },
                new User
                {
                    Username = "willy",
                    Password = "elmago"
                },
                new User
                {
                    Username = "jubero",
                    Password = "pelado7cambios"
                }
            };
        }
    }
}