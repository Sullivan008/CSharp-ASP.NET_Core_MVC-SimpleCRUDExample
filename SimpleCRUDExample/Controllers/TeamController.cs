using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Engine.Interfaces;
using Core.Common.DTOs.Team;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleCRUDExample.Models.Account;
using SimpleCRUDExample.Models.Team;

namespace SimpleCRUDExample.Controllers
{
    public class TeamController : Controller
    {
        private readonly IMapper _mapper;

        private readonly ITeamEngine _teamEngine;

        public TeamController(ITeamEngine teamEngine, IMapper mapper)
        {
            _teamEngine = teamEngine ?? throw new ArgumentNullException(nameof(teamEngine));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Index(RegistrationResultViewModel registrationResult)
        {
            if (registrationResult.IsSuccess != null)
            {
                TempData["RegistrationResult"] = registrationResult;
            }

            return View(_mapper.Map<List<TeamListItemDto>, List<TeamListViewModel>>(_teamEngine.GetAllTeam()));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _teamEngine.TeamIsExists(model.Name))
                {
                    if (await _teamEngine.AddTeam(_mapper.Map<CreateViewModel, TeamDto>(model)))
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = "Failed to save team! Please try again!";

                    return View(model);
                }

                ModelState.AddModelError("Name", "The team name must be unique!");

                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            return View(_mapper.Map<TeamDto, EditViewModel>(_teamEngine.GetTeamById(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _teamEngine.TeamIsExists(model.Id, model.Name))
                {
                    ModelState.AddModelError("Name", "A team with that name already exists!");

                    return View(model);
                }

                if (await _teamEngine.UpdateTeam(_mapper.Map<EditViewModel, TeamDto>(model)))
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Failed to save team! Please try again!";

                return View(model);
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (_teamEngine.DeleteTeamById(id))
            {
                ViewBag.Message = "Failed to delete team! Please try again!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
