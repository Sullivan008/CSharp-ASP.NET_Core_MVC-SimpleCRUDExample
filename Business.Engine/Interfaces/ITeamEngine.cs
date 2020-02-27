using Core.Common.DTOs.Team;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Engine.Interfaces
{
    public interface ITeamEngine
    {
        Task<bool> AddTeam(TeamDto model);

        Task<bool> TeamIsExists(string teamName);

        Task<bool> TeamIsExists(int id, string teamName);

        List<TeamListItemDto> GetAllTeam();

        TeamDto GetTeamById(int id);

        Task<bool> UpdateTeam(TeamDto model);

        bool DeleteTeamById(int id);
    }
}
