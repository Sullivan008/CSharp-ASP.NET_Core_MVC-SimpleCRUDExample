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
        public FormulaContext ctx { get; set; }

        /// <summary>
        ///     Async Method - A paraméterben átadott Team objektumot beszúrja az adatbázisba.
        /// </summary>
        /// <param name="newTeam">A beszúrandó Team objektum.</param>
        /// <returns>A beszúrt Team objektum. Result = NULL - Az objektum beszúrása sikertelen</returns>
        public async Task<Team> Add(Team newTeam)
        {
            try
            {
                ctx.Add(newTeam);

                await ctx.SaveChangesAsync();

                return newTeam;
            }
            catch(Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations("Hiba a TEAM Mentése közben!");

                return null;
            }
        }

        /// <summary>
        ///    Async Method - Lekérdezi az összes Team objektumot az adatbázisból.
        /// </summary>
        /// <returns>
        ///     Az adatbázisból lekérdezett Team objektum halmaz. 
        ///     Result = NULL - Az objektum halmaz lekérdezése sikertelen.
        /// </returns>
        public async Task<IEnumerable<Team>> GetAllTeam()
        {
            try
            {
                return await ctx.Team.ToListAsync();
            }
            catch(Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations("Hiba az összes TEAM lekérdezése közben!");

                return null;
            }
        }

        /// <summary>
        ///     Async Method - A paraméterben átadott ID-jú Team objektum lelkérdezése az adatbázisból.
        /// </summary>
        /// <param name="id">A Team ID-ja, amelyet le kell kérdezni az adatbázisból.</param>
        /// <returns>
        ///     A paraméterben átadott ID-jú adatbázisból lekérdezett Team.
        ///     Result = NULL - Az objektum lekérdezése sikertelen.
        /// </returns>
        public async Task<Team> GetTeamByID(int id)
        {
            try
            {
                return await ctx.Team.FindAsync(id);
            }
            catch (Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations($"Hiba a(z) {id}. sorszámú TEAM lekérdezése közben!");

                return null;
            }
        }

        /// <summary>
        ///     Frissíti a paraméterben átaditt Team objektumot az adatbázisban.
        /// </summary>
        /// <param name="updatedTeam">A frissítendő Team objektum.</param>
        /// <returns>A frissített Team objektum. Result = NULL - Az objektum frissítése sikertelen.</returns>
        public async Task<Team> Update(Team updatedTeam)
        {
            try
            {
                ctx.Update(updatedTeam);

                await ctx.SaveChangesAsync();

                return updatedTeam;
            }
            catch (Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations($"Hiba a(z) {updatedTeam.TeamID}. sorszámú TEAM frissítése közben!");

                return null;
            }
        }

        /// <summary>
        ///     A paraméterben átadott Team objektum törlése az adatbázisból.
        /// </summary>
        /// <param name="deletedTeam">A törlendő Team objektum.</param>
        /// <returns>A törölt Team objektum. Result = NULL - Az objektum törlése sikertelen.</returns>
        public async Task<Team> Delete(Team deletedTeam)
        {
            try
            {
                ctx.Team.Remove(deletedTeam);

                await ctx.SaveChangesAsync();

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
