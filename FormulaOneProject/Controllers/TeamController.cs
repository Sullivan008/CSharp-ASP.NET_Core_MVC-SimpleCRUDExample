using Business.Engine.BusinessEngines.Engines;
using Business.Engine.BusinessEngines.Interfaces;
using Core.Common.Data.DTOs;
using Core.Common.Utils;
using Data.DataAccessLayer.Context;
using FormulaOneProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormulaOneProject.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamEngine teamEngine;

        public TeamController(FormulaContext context)
        {
            teamEngine = new TeamEngine
            {
                _ctx = context
            };
        }

        public async Task<IActionResult> Index()
        {
            return View(TeamDTOToTeamViewModel(await teamEngine.GetAllTeam()));
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                return View(new TeamViewModel() { Years = GetYearsToNowYear(), YearOfFoundation = DateTime.Now.Year });
            }
            else
            {
                return View(TeamDTOToTeamViewModel(teamEngine.GetTeamByID(id)));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TeamID,Name,YearOfFoundation,YearID,NumberOfWinWorldChamp,IsPaidEntryFee")] TeamViewModel team)
        {
            if (ModelState.IsValid)
            {
                TeamDTO dto = TeamViewModelToTeamDTO(team);

                if(dto.TeamID == 0)
                {
                    if(!await teamEngine.AddTeam(dto))
                    {
                        ViewBag.Message = "A TEAM mentése sikertelen! Kérem próbálja újból!";

                        return View(new TeamViewModel() { Years = GetYearsToNowYear(), YearOfFoundation = DateTime.Now.Year });
                    }
                }
                else
                {
                    if(!await teamEngine.UpdateTeam(dto))
                    {
                        ViewBag.Message = "A TEAM frissítése sikertelen! Kérem próbálja újból!";

                        return View(new TeamViewModel() { Years = GetYearsToNowYear(), YearOfFoundation = DateTime.Now.Year });
                    }
                }
    
                return RedirectToAction(nameof(Index));
            }

            return View(team);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if(await teamEngine.DeleteTeamByID(id))
            {
                ViewBag.Message = "A TEAM törlése sikertelen! Kérem próbálja újból!";
            }

            return RedirectToAction(nameof(Index));
        }

        #region HELPER Methods
        private List<Year> GetYearsToNowYear()
        {
            List<Year> years = new List<Year>();

            for(int i = 1900; i <= DateTime.Now.Year; i++)
            {
                years.Add(new Year() { YearID = i, YearOfFoundation = i.ToString() });
            }

            return years;
        }

        private List<TeamViewModel> TeamDTOToTeamViewModel(List<TeamDTO> teamDTOs)
        {
            List<TeamViewModel> teamVMs = new List<TeamViewModel>();

            foreach (TeamDTO item in teamDTOs)
            {
                TeamViewModel current = new TeamViewModel();

                PropertyMapper.MapProperties(item, current);

                teamVMs.Add(current);
            }

            return teamVMs;
        }

        private TeamViewModel TeamDTOToTeamViewModel(TeamDTO teamDTO)
        {
            TeamViewModel teamVM = new TeamViewModel() { Years = GetYearsToNowYear() };

            PropertyMapper.MapProperties(teamDTO, teamVM);

            return teamVM;
        }

        private TeamDTO TeamViewModelToTeamDTO(TeamViewModel teamViewModel)
        {
            TeamDTO teamDTO = new TeamDTO();

            PropertyMapper.MapProperties(teamViewModel, teamDTO);

            return teamDTO;
        }
        #endregion
    }
}
