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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Team>()
                .HasIndex(o => new { o.Name })
                .IsUnique();

            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Team> Team { get; set; }
    }
}
