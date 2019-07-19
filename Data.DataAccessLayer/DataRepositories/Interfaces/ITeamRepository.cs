using Business.Entities.DataBaseEntities;
using Data.DataAccessLayer.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.DataAccessLayer.DataRepositories.Interfaces
{
    public interface ITeamRepository
    {
        /// <summary>
        ///     A Contextet tartalmazó objektum
        /// </summary>
        FormulaContext ctx { get; set; }

        /// <summary>
        ///     A paraméterben átadott Team objektumot beszúrja az adatbázisba.
        /// </summary>
        /// <param name="newTeam">A beszúrandó Team objektum.</param>
        /// <returns>A beszúrt Team objektum.</returns>
        Task<Team> Add(Team newTeam);

        /// <summary>
        ///     Lekérdezi az összes Team objektumot az adatbázisból.
        /// </summary>
        /// <returns>Az adatbázisból lekérdezett Team objektum halmaz.</returns>
        Task<IEnumerable<Team>> GetAllTeam();

        /// <summary>
        ///     A paraméterben átadott ID-jú Team objektum lelkérdezése az adatbázisból.
        /// </summary>
        /// <param name="id">A Team ID-ja, amelyet le kell kérdezni az adatbázisból.</param>
        /// <returns>A paraméterben átadott ID-jú adatbázisból lekérdezett Team.</returns>
        Task<Team> GetTeamByID(int id);

        /// <summary>
        ///     Frissíti a paraméterben átaditt Team objektumot az adatbázisban.
        /// </summary>
        /// <param name="updatedTeam">A frissítendő Team objektum.</param>
        /// <returns>A frissített Team objektum.</returns>
        Task<Team> Update(Team updatedTeam);

        /// <summary>
        ///     A paraméterben átadott Team objektum törlése az adatbázisból.
        /// </summary>
        /// <param name="deletedTeam">A törlendő Team objektum.</param>
        /// <returns>A törölt Team objektum.</returns>
        Task<Team> Delete(Team deletedTeam);
    }
}
