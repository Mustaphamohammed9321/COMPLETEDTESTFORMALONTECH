

using MALON_GLOBAL_WEBAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace MALON_GLOBAL_WEBAPP.Data
{
    public class ProjDbContext : DbContext
    {
        public ProjDbContext(DbContextOptions<ProjDbContext> options) : base(options)
        {
                
        }


        public DbSet<UserAccount> UserAccounts { get; set; }



    }
}
