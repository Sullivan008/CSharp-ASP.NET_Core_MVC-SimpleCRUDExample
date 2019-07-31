using Business.Engine.BusinessEngines.Engines;
using Business.Engine.BusinessEngines.Interfaces;
using Core.Common.Data.DTOs;
using Core.Common.Utils;
using Data.DataAccessLayer.Context;
using FormulaOneProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormulaOneProject.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamEngine teamEngine;

        /// <summary>
        ///     Konstruktor.
        /// </summary>
        /// <param name="context">Az alkalmazás alapul szolgáló adatbázisa.</param>
        public TeamController(FormulaContext context)
        {
            /// A paraméterben átadott adatbázis objektum átadása az Engine-nek.
            teamEngine = new TeamEngine
            {
                ctx = context
            };
        }

        #region GET Methods
        /// <summary>
        ///    Async Method - Az adatbázis adatait megjelenítendő felület betöltésére szolgáló Controller Method.
        /// </summary>
        /// <returns>Team/Index View</returns>
        public async Task<IActionResult> Index()
        {
            /// A TeamViewModel objektumokká átmappelt Engine által visszatérített TeamDTO adathalmaz átadása a View-nak.
            return View(TeamDTOToTeamViewModel(await teamEngine.GetAllTeam()));
        }

        /// <summary>
        ///     Method - Egy új Team vagy már meglévő Team elkészítéséhez szükséges felület betöltésére szolgáló Controller Method.
        /// </summary>
        /// <param name="id">A szerkesztendő TeamID-ja. Default Value = 0</param>
        /// <returns>Team/AddOrEdit View</returns>
        [Authorize]
        public IActionResult AddOrEdit(int id = 0)
        {
            /// Ha az ID 0, akkor új Team objektumot kell elkészíteni, ellenkező esetben meglévő Team objektumot kell szerkeszteni.
            if(id == 0)
            {
                /// Új TeamViewModel objektum átadása a View-nak.
                return View(new TeamViewModel() { Years = GetYearsToNowYear(), YearOfFoundation = DateTime.Now.Year });
            }
            else
            {
                /// TeamViewModel objektummá átmappelt Engine által visszatérített paraméterben átadott ID-jú TeamDTO átadása a View-nak.
                return View(TeamDTOToTeamViewModel(teamEngine.GetTeamByID(id)));
            }
        }

        /// <summary>
        ///     Async Method - Egy Team törlésére szolgáló Controller Method.
        /// </summary>
        /// <param name="id">A törlendő Team ID-ja</param>
        /// <returns>Team/Index View</returns>
        public async Task<IActionResult> Delete(int id)
        {
            /// Az Engine által visszatérített Törlési eljárásnak az eredménye alapján meghatározzuk,
            /// hogy írunk-e ki a felhasználónak hibaüzenetet, vagy sem.
            if (await teamEngine.DeleteTeamByID(id))
            {
                ViewBag.Message = "A TEAM törlése sikertelen! Kérem próbálja újból!";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region POST Methods
        /// <summary>
        ///     POST Async Method - A paraméterben kapott modell mentésére vagy frissítésére szolgáló Controller Method.
        /// </summary>
        /// <param name="team">A mentendő vagy frissítendő objektum.</param>
        /// <returns>
        ///     Sikeresség esetén - Navigation -> Team/Index View
        ///     Sikertelenség esetén -> Team/AddOrEdit View
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddOrEdit([Bind("TeamID,Name,YearOfFoundation,YearID,NumberOfWinWorldChamp,IsPaidEntryFee")] TeamViewModel team)
        {
            /// Vizsgálat, hogy a paraméterben kapott Modell, minden követelménynek megfelel.
            if (ModelState.IsValid)
            {
                /// A paraméterben kapott ViewModel-ből készítünk egy DTO-t, amelyet az Engine-nek fogunk továbbítani.
                TeamDTO dto = TeamViewModelToTeamDTO(team);

                /// Vizsgálat, hogy a DTO tartalmaz-e ID-t. Ha annak 0 az értéke, akkor Új objektumot szúrunk be az adatbázisba,
                /// ellenkező esetben egy meglévőt szerkesztünk.
                if(dto.TeamID == 0)
                {
                    /// Az Engine által visszatérített Beszúrási eljárásnak az eredménye alapján meghatározzuk,
                    /// hogy írunk-e ki a felhasználónak hibaüzenetet, vagy sem.
                    if (!await teamEngine.AddTeam(dto))
                    {
                        ViewBag.Message = "A TEAM mentése sikertelen! Kérem próbálja újból!";

                        return View(new TeamViewModel() { Years = GetYearsToNowYear(), YearOfFoundation = DateTime.Now.Year });
                    }
                }
                else
                {
                    /// Az Engine által visszatérített Frissítési eljárásnak az eredménye alapján meghatározzuk,
                    /// hogy írunk-e ki a felhasználónak hibaüzenetet, vagy sem.
                    if (!await teamEngine.UpdateTeam(dto))
                    {
                        ViewBag.Message = "A TEAM frissítése sikertelen! Kérem próbálja újból!";

                        return View(new TeamViewModel() { Years = GetYearsToNowYear(), YearOfFoundation = DateTime.Now.Year });
                    }
                }
    
                return RedirectToAction(nameof(Index));
            }

            return View(team);
        }
        #endregion

        #region HELPER Methods
        /// <summary>
        ///     Metódus, amely egy számokból álló listát készít el. Ez a lista fogja tartalmazni,
        ///     a felületen megjelenő Évszámok listáját. Az évszámok 1900-tól kezdődik, az aktuális évig bezárva.
        /// </summary>
        /// <returns>Évszámokat tartalmazó Lista.</returns>
        private List<Year> GetYearsToNowYear()
        {
            List<Year> years = new List<Year>();

            for(int i = 1900; i <= DateTime.Now.Year; i++)
            {
                years.Add(new Year() { YearID = i, YearOfFoundation = i.ToString() });
            }

            return years;
        }

        /// <summary>
        ///     A paraméterben átadott TeamDTO objektum listát, átmappeli
        ///     TeamViewModel objektum Listává.
        /// </summary>
        /// <param name="teamDTOs">Az átmappelendő TeamDTO objektum lista</param>
        /// <returns>A sikeresen átmappelt TeamViewModel Objektum Lista.</returns>
        private List<TeamViewModel> TeamDTOToTeamViewModel(List<TeamDTO> teamDTOs)
        {
            /// Objektumokat tartalmazó Lista példányosítása.
            List<TeamViewModel> teamVMs = new List<TeamViewModel>();

            /// Az átmappelendő TEAM objektum halmazt bejárva végre hajtjuk a mappelést.
            foreach (TeamDTO item in teamDTOs)
            {
                TeamViewModel current = new TeamViewModel();

                PropertyMapper.MapProperties(item, current);

                teamVMs.Add(current);
            }

            return teamVMs;
        }

        /// <summary>
        ///     A paraméterben átadott TeamDTO objektumot átmappeli TeamViewModel objektummá.
        /// </summary>
        /// <param name="teamDTO">Az átmapelendő TeamDTO ojbektum.</param>
        /// <returns>A sikeresen átmappelt TeamViewModel objektum.</returns>
        private TeamViewModel TeamDTOToTeamViewModel(TeamDTO teamDTO)
        {
            TeamViewModel teamVM = new TeamViewModel() { Years = GetYearsToNowYear() };

            PropertyMapper.MapProperties(teamDTO, teamVM);

            return teamVM;
        }

        /// <summary>
        ///     A paraméterben átadott TeamViewModel objektumot átmappeli TeamDTO objektummá.
        /// </summary>
        /// <param name="teamViewModel">Az átmapelendő TeamViewModel objektum.</param>
        /// <returns>A sikeresen átmappelt TeamDTO objektum.</returns>
        private TeamDTO TeamViewModelToTeamDTO(TeamViewModel teamViewModel)
        {
            TeamDTO teamDTO = new TeamDTO();

            PropertyMapper.MapProperties(teamViewModel, teamDTO);

            return teamDTO;
        }
        #endregion
    }
}
