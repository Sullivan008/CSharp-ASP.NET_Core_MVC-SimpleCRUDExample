using Business.Engine.BusinessEngines.Engines;
using Business.Engine.BusinessEngines.Interfaces;
using Business.Entities.DataBaseEntities;
using FormulaOneProject.Models;
using FormulaOneProject.Models.ConfigModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FormulaOneProject.Controllers
{
    public class AccountController : Controller
    {
        private IUserEngine _userEngine;
        
        private UserManager<AppUser> UserManager { get; set; }

        private SignInManager<AppUser> SignInManager { get; set; }

        private readonly IOptions<LoginConfigModel> _loginConfig;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<LoginConfigModel> loginConfig)
        {
            UserManager = userManager;

            SignInManager = signInManager;

            _loginConfig = loginConfig;

            _userEngine = new UserEngine();
        }

        public async Task<IActionResult> Register()
        {
            bool? result = await _userEngine.Register(UserManager, _loginConfig.Value.UserName, _loginConfig.Value.Password);

            if (result == null)
            {
                ViewBag.Message = "A teszt felhasználó regisztrálva lett! A felhasználó már be tud jelentkezni (UN: admin, PW: f1test2018)!";
            }
            else if (result == true)
            {
                ViewBag.Message = "A teszt felhasználó regisztrációja sikeres! A felhasználó már be tud jelentkezni (UN: admin, PW: f1test2018)!";
            }
            else if (result == false)
            {
                ViewBag.Message = "A teszt felhasználó regisztrációja során hiba keletkezett!";
            }

            return RedirectToAction("Index", "Team");
        }

        public IActionResult NavigateLogin()
        {
            return RedirectToAction("Index", "Login");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login([Bind("UserName,Password")] LoginViewModel userCredentials)
        {
            bool result = await _userEngine.Login(SignInManager, userCredentials.UserName, userCredentials.Password);

            if(result)
            {
                return RedirectToAction("Index", "Team");
            }

            return RedirectToAction("Index", "Login", new { message = "A megadott adatok nem megfelelőek!" });
        }

        public async Task<IActionResult> Logout()
        {
            await _userEngine.Logout(SignInManager);

            return RedirectToAction("Index", "Team");
        }
    }
}