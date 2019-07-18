using Business.Entities.DataBaseEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccessLayer.Context
{
    public class FormulaContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public FormulaContext()
        {

        }

        public FormulaContext(DbContextOptions<FormulaContext> options) : base(options)
        {

        }

        public DbSet<Team> Team { get; set; }
    }
}
