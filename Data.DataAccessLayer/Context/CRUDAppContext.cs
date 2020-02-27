using Data.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccessLayer.Context
{
    public class CRUDAppContext : IdentityDbContext<CRUDAppUser, CRUDAppRole, int>
    {
        public CRUDAppContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasIndex(team => new { team.Name })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        #region DbSets

        public DbSet<Team> Team { get; set; }

        #endregion
    }
}
