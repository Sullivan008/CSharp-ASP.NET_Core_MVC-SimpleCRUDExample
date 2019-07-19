using Business.Entities.DataBaseEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccessLayer.Context
{
    public class FormulaContext : IdentityDbContext<AppUser, AppRole, int>
    {
        /// <summary>
        ///     Konstruktor
        /// </summary>
        public FormulaContext()
        {

        }

        /// <summary>
        ///     Konstruktor, amely az IdentityDbContext-ből származik
        /// </summary>
        /// <param name="options">A Context-hez tartozó beállítások</param>
        public FormulaContext(DbContextOptions<FormulaContext> options) : base(options)
        {

        }

        /// <summary>
        ///     A metódus akkor fog meghívódni, amikor az adott Context-hez tartozó modell már inicializálva lett, de még mielőtt
        ///         a modell le lenne zárva, hogy a Context-ust inicializálja. Úgynevezett két állapot közötti lefutandó metódus.
        ///     A Default Implementációja nem csinál semmit, de egy leszármazott osztályból felüldeffiniálható, így egy modell
        ///         konfigurálható mielőtt még lezárná az Entity Framework.
        /// </summary>
        /// <param name="modelBuilder">A Builder ami a Modell-t deffiniálja az adott Context-hez.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// Unique Definíció a Team objektum Name property-jéhez.
            modelBuilder.Entity<Team>()
                .HasIndex(o => new { o.Name })
                .IsUnique();

            /// Az ŐS osztályhoz tartozó model definíciók lefuttatása.
            base.OnModelCreating(modelBuilder);
        }

        #region DbSets
        /// <summary>
        ///     DbSetek inicializálása az EntityFramework-höz.
        /// </summary>
        
        
        public DbSet<Team> Team { get; set; }
        #endregion
    }
}
