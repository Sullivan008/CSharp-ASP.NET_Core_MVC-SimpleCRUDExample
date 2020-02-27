using Business.Engine.Interfaces;
using Core.ApplicationCore.UnitOfWork;
using Core.Common.DTOs.Team;
using Data.DataAccessLayer.Context;
using Data.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Engine.Engines
{
    public class TeamEngine : ITeamEngine
    {
        private readonly IUnitOfWork<CRUDAppContext> _unitOfWork;

        public TeamEngine(IUnitOfWork<CRUDAppContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddTeam(TeamDto model)
        {
            Team team = await _unitOfWork.GetRepository<Team>().AddAsync(new Team
            {
                Name = model.Name,
                IsPaidEntryFee = model.IsPaidEntryFee,
                NumberOfWinWorldChamp = model.NumberOfWonWorldChampionships,
                YearOfFoundation = model.YearOfFoundation
            });

            return team != null && team.TeamId != 0;
        }

        public async Task<bool> TeamIsExists(string teamName) => 
            await _unitOfWork.GetRepository<Team>().FindAsync(x => string.Equals(x.Name, teamName, StringComparison.CurrentCultureIgnoreCase)) != null;

        public async Task<bool> TeamIsExists(int id, string teamName) =>
            await _unitOfWork.GetRepository<Team>().FindAsync(x => x.TeamId != id && string.Equals(x.Name, teamName, StringComparison.CurrentCultureIgnoreCase)) != null;

        public List<TeamListItemDto> GetAllTeam()
        {
            IEnumerable<Team> teams =
                _unitOfWork.GetRepository<Team>().Filter(null, x => x.OrderBy(y => y.Name));

            return teams.Select(x => new TeamListItemDto
            {
                Id = x.TeamId,
                Name = x.Name,
                IsPaidEntryFee = x.IsPaidEntryFee,
                NumberOfWonWorldChampionships = x.NumberOfWinWorldChamp,
                YearOfFoundation = x.YearOfFoundation
            }).ToList();
        }

        public TeamDto GetTeamById(int id)
        {
            Team team = _unitOfWork.GetRepository<Team>().FindAsync(x => x.TeamId == id).Result;

            if (team == null)
            {
                return new TeamDto();
            }

            return new TeamDto
            {
                Name = team.Name,
                Id = team.TeamId,
                NumberOfWonWorldChampionships = team.NumberOfWinWorldChamp,
                YearOfFoundation = team.YearOfFoundation,
                IsPaidEntryFee = team.IsPaidEntryFee
            };
        }

        public async Task<bool> UpdateTeam(TeamDto model)
        {
            Team team = await _unitOfWork.GetRepository<Team>().UpdateAsync(new Team
            {
                TeamId = model.Id,
                Name = model.Name,
                NumberOfWinWorldChamp = model.NumberOfWonWorldChampionships,
                YearOfFoundation = model.YearOfFoundation,
                IsPaidEntryFee = model.IsPaidEntryFee
            });

            return team != null;
        }

        public bool DeleteTeamById(int id)
        {
            Task<int> result = _unitOfWork.GetRepository<Team>().DeleteAsync(new Team { TeamId = id });

            return Convert.ToBoolean(result.Result);
        }
    }
}
