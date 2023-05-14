using ClientProject.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OpenBankingCore
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public DbSet<OpenBankingClient> OpenBankingClients { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts)
        {

        }
        
    }
}
