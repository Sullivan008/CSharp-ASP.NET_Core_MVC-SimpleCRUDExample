using Business.Entities.DataBaseEntities;
using Core.Common.Data.DTOs;
using Data.DataAccessLayer.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Engine.BusinessEngines.Interfaces
{
    public interface ITeamEngine
    {
        FormulaContext _ctx { get; set; }

        Task<List<TeamDTO>> GetAllTeam();

        TeamDTO GetTeamByID(int id);

        Task<bool> AddTeam(TeamDTO team);

        Task<bool> UpdateTeam(TeamDTO team);

        Task<bool> DeleteTeamByID(int id);
    }
}
