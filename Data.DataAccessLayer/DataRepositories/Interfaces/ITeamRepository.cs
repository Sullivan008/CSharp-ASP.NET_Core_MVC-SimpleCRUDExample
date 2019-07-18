using Business.Entities.DataBaseEntities;
using Data.DataAccessLayer.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.DataAccessLayer.DataRepositories.Interfaces
{
    public interface ITeamRepository
    {
        FormulaContext _ctx { get; set; }
        Task<Team> Add(Team newTeam);

        Task<IEnumerable<Team>> GetAllTeam();

        Task<Team> GetTeamByID(int id);

        Task<Team> Update(Team updatedTeam);

        Task<Team> Delete(Team deletedTeam);
    }
}
