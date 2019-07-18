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

        public FormulaContext _ctx { get; set; }

        /// <summary>
        ///     Konstruktor
        /// </summary>
        public TeamEngine()
        {
            _teamRepository = new TeamRepository();
        }

        public async Task<bool> AddTeam(TeamDTO teamDTO)
        {
            _teamRepository._ctx = _ctx;

            Team insertedTeam = await _teamRepository.Add(MapTeamDTOToTeam(teamDTO));

            if (insertedTeam != null && insertedTeam?.TeamID != 0)
            {
                return true;
            }

            return false;
        }

        public async Task<List<TeamDTO>> GetAllTeam()
        {
            _teamRepository._ctx = _ctx;

            return MapTeamToTeamDTO((await _teamRepository.GetAllTeam()).ToList());
        }

        public TeamDTO GetTeamByID(int id)
        {
            _teamRepository._ctx = _ctx;

            TeamDTO foundTeam = MapTeamToTeamDTO(_teamRepository.GetTeamByID(id).Result);

            if (foundTeam != null && foundTeam?.TeamID != 0)
            {
                return foundTeam;
            }
            else
            {
                return new TeamDTO();
            }
        }

        public async Task<bool> UpdateTeam(TeamDTO teamDTO)
        {
            _teamRepository._ctx = _ctx;

            Team updatedTeam = await _teamRepository.Update(MapTeamDTOToTeam(teamDTO));

            if(updatedTeam != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteTeamByID(int id)
        {
            _teamRepository._ctx = _ctx;
            
            Team deletedTeam = await _teamRepository.Delete(new Team() { TeamID = id });

            if(deletedTeam != null && deletedTeam?.TeamID != 0)
            {
                return true;
            }

            return false;
        }

        #region HELPER Methods
        private List<TeamDTO> MapTeamToTeamDTO(List<Team> teams)
        {
            List<TeamDTO> teamDTOs = new List<TeamDTO>();

            foreach (Team item in teams)
            {
                TeamDTO dto = new TeamDTO();

                PropertyMapper.MapProperties(item, dto);

                teamDTOs.Add(dto);
            }

            return teamDTOs;
        }

        private Team MapTeamDTOToTeam(TeamDTO teamDTO)
        {
            Team team = new Team();

            PropertyMapper.MapProperties<TeamDTO, Team>(teamDTO, team);

            return team;
        }

        private TeamDTO MapTeamToTeamDTO(Team team)
        {
            TeamDTO teamDTO = new TeamDTO();

            PropertyMapper.MapProperties<Team, TeamDTO>(team, teamDTO);

            return teamDTO;
        }
        #endregion
    }
}
