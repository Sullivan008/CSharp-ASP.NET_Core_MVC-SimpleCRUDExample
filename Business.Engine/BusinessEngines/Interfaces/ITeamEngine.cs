using Core.Common.Data.DTOs;
using Data.DataAccessLayer.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Engine.BusinessEngines.Interfaces
{
    public interface ITeamEngine
    {
        /// <summary>
        ///     A Contextet tartalmazó objektum
        /// </summary>
        FormulaContext ctx { get; set; }

        /// <summary>
        ///     Metódus, amely a paraméterben átadott TEAM objektumot beszúrja az adatbázisba.
        /// </summary>
        /// <param name="teamDTO">Az adatbázisba beszúrandó TEAM objektum.</param>
        /// <returns>TRUE - Ha sikeres volt a beszúrás | FALSE - Ha nem.</returns>
        Task<bool> AddTeam(TeamDTO teamDTO);

        /// <summary>
        ///     Metódus, amely lekérdezi az összes TEAM objektumot az adatbázisból.
        /// </summary>
        /// <returns>A TEAM objektumokat tartalmazó lista.</returns>
        Task<List<TeamDTO>> GetAllTeam();

        /// <summary>
        ///     Metódus, amely lekérdezi a paraméterben átadott ID-jú TEAM-et az adatbázisóbl.
        /// </summary>
        /// <param name="id">A lekérdezendő TEAM ID-ja.</param>
        /// <returns>Az adatbázisból lekérdezett TEAM objektum.</returns>
        TeamDTO GetTeamByID(int id);

        /// <summary>
        ///     Metódus, amely a paraméterben átadott TEAM objektumot frissíti az adatbázisban.
        /// </summary>
        /// <param name="team">A frissítendő TEAM ID-ja.</param>
        /// <returns>TRUE - Ha sikeres volt a frissítés | FALSE - Ha nem.</returns>
        Task<bool> UpdateTeam(TeamDTO team);

        /// <summary>
        ///     Metódus, amely a paraméterben átadott ID-jú TEAM-et kitörli az adatbázisból
        /// </summary>
        /// <param name="id">A törelndő TEAM ID-ja.</param>
        /// <returns>TRUE - Ha sikeres volt a törlés | FALSE - Ha nem.</returns>
        Task<bool> DeleteTeamByID(int id);
    }
}
