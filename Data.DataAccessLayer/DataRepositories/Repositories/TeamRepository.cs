using Business.Entities.DataBaseEntities;
using Core.Handlers.BackEndExceptionHandler;
using Data.DataAccessLayer.Context;
using Data.DataAccessLayer.DataRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.DataAccessLayer.DataRepositories.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        public FormulaContext _ctx { get; set; }

        public async Task<Team> Add(Team newTeam)
        {
            try
            {
                _ctx.Add(newTeam);

                await _ctx.SaveChangesAsync();

                return newTeam;
            }
            catch(Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations("Hiba a TEAM Mentése közben!");

                return null;
            }
        }
        public async Task<IEnumerable<Team>> GetAllTeam()
        {
            try
            {
                return await _ctx.Team.ToListAsync();
            }
            catch(Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations("Hiba az összes TEAM lekérdezése közben!");

                return null;
            }
        }

        public async Task<Team> GetTeamByID(int id)
        {
            try
            {
                return await _ctx.Team.FindAsync(id);
            }
            catch (Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations($"Hiba a(z) {id}. sorszámú TEAM lekérdezése közben!");

                return null;
            }
        }

        public async Task<Team> Update(Team updatedTeam)
        {
            try
            {
                _ctx.Update(updatedTeam);

                await _ctx.SaveChangesAsync();

                return updatedTeam;
            }
            catch (Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations($"Hiba a(z) {updatedTeam.TeamID}. sorszámú TEAM frissítése közben!");

                return null;
            }
        }

        public async Task<Team> Delete(Team deletedTeam)
        {
            try
            {
                _ctx.Team.Remove(deletedTeam);

                await _ctx.SaveChangesAsync();

                return deletedTeam;
            }
            catch (Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations($"Hiba a(z) {deletedTeam.TeamID}. sorszámú TEAM törlése közben közben!");

                return null;
            }
        }
    }
}
