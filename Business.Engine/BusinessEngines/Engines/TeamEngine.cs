using Business.Engine.BusinessEngines.Interfaces;
using Business.Entities.DataBaseEntities;
using Core.Common.Data.DTOs;
using Core.Common.Utils;
using Data.DataAccessLayer.Context;
using Data.DataAccessLayer.DataRepositories.Interfaces;
using Data.DataAccessLayer.DataRepositories.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Engine.BusinessEngines.Engines
{
    public class TeamEngine : ITeamEngine
    {
        private readonly ITeamRepository _teamRepository;

        public FormulaContext ctx { get; set; }

        /// <summary>
        ///     Konstruktor
        /// </summary>
        public TeamEngine()
        {
            /// Repository példányosítása.
            _teamRepository = new TeamRepository();
        }

        /// <summary>
        ///     Async Method - Metódus, amely a paraméterben átadott TEAM objektumot beszúrja az adatbázisba.
        /// </summary>
        /// <param name="team">Az adatbázisba beszúrandó TEAM objektum.</param>
        /// <returns>TRUE - Ha sikeres volt a beszúrás | FALSE - Ha nem.</returns>
        public async Task<bool> AddTeam(TeamDTO teamDTO)
        {
            /// A context objektum átadása a Repository számára.
            _teamRepository.ctx = ctx;

            /// Az átmappelt objektum beszúrása az adatbázisba.
            Team insertedTeam = await _teamRepository.Add(MapTeamDTOToTeam(teamDTO));

            /// Vizsgálat, hogy jött-e vissza TEAM objektum, és ha igen, akkor annak van-e ID-ja.
            if (insertedTeam != null && insertedTeam?.TeamID != 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Async Method - Metódus, amely lekérdezi az összes TEAM objektumot az adatbázisból.
        /// </summary>
        /// <returns>A TEAM objektumokat tartalmazó lista.</returns>
        public async Task<List<TeamDTO>> GetAllTeam()
        {
            /// A context objektum átadása a Repository számára.
            _teamRepository.ctx = ctx;

            /// Az adatbázisból megkapott átmappelt TEAM objektum visszatérítése.
            return MapTeamToTeamDTO((await _teamRepository.GetAllTeam()).ToList());
        }

        /// <summary>
        ///     Metódus, amely lekérdezi a paraméterben átadott ID-jú TEAM-et az adatbázisóbl.
        /// </summary>
        /// <param name="id">A lekérdezendő TEAM ID-ja.</param>
        /// <returns>Az adatbázisból lekérdezett TEAM objektum.</returns>
        public TeamDTO GetTeamByID(int id)
        {
            /// A context objektum átadása a Repository számára.
            _teamRepository.ctx = ctx;

            /// Az adatbázisból lekérdezett TEAM objektum átmappelése.
            TeamDTO foundTeam = MapTeamToTeamDTO(_teamRepository.GetTeamByID(id).Result);

            /// Vizsgálat, hogy jött-e vissza TEAM objektum, és ha igen, akkor annak van-e ID-ja.
            if (foundTeam != null && foundTeam?.TeamID != 0)
            {
                return foundTeam;
            }
            else
            {
                return new TeamDTO();
            }
        }

        /// <summary>
        ///    Async Method - Metódus, amely a paraméterben átadott TEAM objektumot frissíti az adatbázisban.
        /// </summary>
        /// <param name="team">A frissítendő TEAM ID-ja.</param>
        /// <returns>TRUE - Ha sikeres volt a frissítés | FALSE - Ha nem.</returns>
        public async Task<bool> UpdateTeam(TeamDTO teamDTO)
        {
            /// A context objektum átadása a Repository számára.
            _teamRepository.ctx = ctx;

            /// A paraméterben kapott TEAM objektum átmappelése, majd frissítése az adatbázisban.
            Team updatedTeam = await _teamRepository.Update(MapTeamDTOToTeam(teamDTO));

            /// Vizsgálat, hogy jött-e vissza updated objektum a Repositorytól.
            if(updatedTeam != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Async Method - Metódus, amely a paraméterben átadott ID-jú TEAM-et kitörli az adatbázisból
        /// </summary>
        /// <param name="id">A törelndő TEAM ID-ja.</param>
        /// <returns>TRUE - Ha sikeres volt a törlés | FALSE - Ha nem.</returns>
        public async Task<bool> DeleteTeamByID(int id)
        {
            /// A context objektum átadása a Repository számára.
            _teamRepository.ctx = ctx;
            
            /// A paraméterben átadott ID-jú TEAM objektum törlése.
            Team deletedTeam = await _teamRepository.Delete(new Team() { TeamID = id });

            /// Vizsgálat, hogy tartalmaz-e a Repository által visszatérített RRESUlT értéket, és ha igen
            /// akkor van-e ID érték beállítva neki.
            if(deletedTeam != null && deletedTeam?.TeamID != 0)
            {
                return true;
            }

            return false;
        }

        #region HELPER Methods
        /// <summary>
        ///     A paraméterben átadott TEAM objektum listát, átmappeli
        ///     TEAMDTO objektum Listává.
        /// </summary>
        /// <param name="teams">Az átmappelendő TEAM objektum lista</param>
        /// <returns>A sikeresen átmappelt TEAMDTO Objektum Lista.</returns>
        private List<TeamDTO> MapTeamToTeamDTO(List<Team> teams)
        {
            /// Objektumokat tartalmazó Lista példányosítása.
            List<TeamDTO> teamDTOs = new List<TeamDTO>();

            /// Az átmappelendő TEAM objektum halmazt bejárva végre hajtjuk a mappelést.
            foreach (Team item in teams)
            {
                TeamDTO dto = new TeamDTO();

                PropertyMapper.MapProperties(item, dto);

                teamDTOs.Add(dto);
            }

            return teamDTOs;
        }

        /// <summary>
        ///     A paraméterben átadott TEAMDTO objektumot átmappeli TEAM objektummá.
        /// </summary>
        /// <param name="teamDTO">Az átmapelendő TEAMDTO ojbektum.</param>
        /// <returns>A sikeresen átmappelt Team objektum.</returns>
        private Team MapTeamDTOToTeam(TeamDTO teamDTO)
        {
            Team team = new Team();

            PropertyMapper.MapProperties<TeamDTO, Team>(teamDTO, team);

            return team;
        }

        /// <summary>
        ///     A paraméterben átadott TEAM objektumot átmappeli TEAMDTO objektummá.
        /// </summary>
        /// <param name="team">Az átmapelendő TEAM ojbektum.</param>
        /// <returns>A sikeresen átmappelt TeamDTO objektum.</returns>
        private TeamDTO MapTeamToTeamDTO(Team team)
        {
            TeamDTO teamDTO = new TeamDTO();

            PropertyMapper.MapProperties<Team, TeamDTO>(team, teamDTO);

            return teamDTO;
        }
        #endregion
    }
}
