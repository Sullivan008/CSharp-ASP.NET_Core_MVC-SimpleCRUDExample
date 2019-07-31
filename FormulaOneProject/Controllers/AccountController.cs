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
        private readonly IOptions<LoginConfigModel> _loginConfig;
        
        private UserManager<AppUser> UserManager { get; set; }
        private SignInManager<AppUser> SignInManager { get; set; }

        /// <summary>
        ///     Konstruktor.
        /// </summary>
        /// <param name="userManager">A felhasználók kezeléséhez szükséges objektum.</param>
        /// <param name="signInManager">A bejelentkeztetés megvalósításához szükséges objektum.</param>
        /// <param name="loginConfig">Konfigurációs objektum</param>
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<LoginConfigModel> loginConfig)
        {
            /// Objektumok inicialiázlása.
            UserManager = userManager;
            SignInManager = signInManager;
            _loginConfig = loginConfig;

            /// Objektum példányosítása.
            _userEngine = new UserEngine();
        }

        #region GET Methods
        /// <summary>
        ///     Async Method - A Regisztráció megvalósítását tartalmazó Controller Method.
        /// </summary>
        /// <returns>Navigation -> Team/Index</returns>
        public async Task<IActionResult> Register()
        {
            /// Engine metódus hívás, amely a regisztráció üzleti logikáját tartalmazza.
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

        /// <summary>
        ///     Method - A Navigációt valósítja meg a Login/Index oldalra.
        /// </summary>
        /// <returns>Navigation -> Login/Index</returns>
        [HttpGet("[controller]/Login")]
        public IActionResult NavigateLogin()
        {
            return RedirectToAction("Index", "Login");
        }

        /// <summary>
        ///     Async Method - A Kijelentkeztetés megvalósítását tartalmazó Controller Method.
        /// </summary>
        /// <returns>Navigation -> Team/Index</returns>
        public async Task<IActionResult> Logout()
        {
            /// Engine metódus hívás, amely a kijelentkeztetés üzleti logikáját tartalmazza.
            await _userEngine.Logout(SignInManager);

            return RedirectToAction("Index", "Team");
        }
        #endregion

        #region POST Methods
        /// <summary>
        ///     POST Async Method - A Bejelentkezés megvalósítását tartalmazó Controller Method.
        /// </summary>
        /// <param name="userCredentials">A bejelentkezéshez szükséges felhasználói adatok.</param>
        /// <returns>
        ///     Sikeresség esetén - Navigation -> Team/Index
        ///     Sikertelenség esetén - Navigation -> Login/Index?message=message
        /// </returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login([Bind("UserName,Password")] LoginViewModel userCredentials)
        {
            /// Engine metódus hívás, amely a bejelentkeztetés üzleti logikáját tartalmazza.
            bool result = await _userEngine.Login(SignInManager, userCredentials.UserName, userCredentials.Password);

            if(result)
            {
                return RedirectToAction("Index", "Team");
            }

            return RedirectToAction("Index", "Login", new { message = "A megadott adatok nem megfelelőek!" });
        }
        #endregion
    }
}