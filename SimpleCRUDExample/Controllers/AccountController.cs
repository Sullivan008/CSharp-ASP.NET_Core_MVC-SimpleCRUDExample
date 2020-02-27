using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Engine.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using SimpleCRUDExample.Core.Models.ConfigModels;
using SimpleCRUDExample.Models.Account;

namespace SimpleCRUDExample.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserEngine _userEngine;

        private readonly IOptions<LoginConfigModel> _loginConfig;

        public AccountController(IOptions<LoginConfigModel> loginConfig, IUserEngine userEngine)
        {
            _loginConfig = loginConfig ?? throw new ArgumentNullException(nameof(loginConfig));
            _userEngine = userEngine ?? throw new ArgumentNullException(nameof(userEngine));
        }

        public async Task<IActionResult> Register()
        {
            bool registrationResult = await _userEngine.Register(_loginConfig.Value.UserName, _loginConfig.Value.Password);

            return RedirectToAction("Index", "Team",
                new RouteValueDictionary(GetRegistrationResultViewModel(registrationResult)));
        }

        [HttpGet("[controller]/Login")]
        public IActionResult NavigateLogin(string returnUrl)
        {
            return RedirectToAction("Index", "Login", new {ReturnUrl = returnUrl});
        }

        public async Task<IActionResult> Logout()
        {
            await _userEngine.Logout();

            return RedirectToAction("Index", "Team");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool result = await _userEngine.Login(model.UserName, model.Password);

                if (result && !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return result ? RedirectToAction("Index", "Team") :
                    RedirectToAction("Index", "Login", new {message = "<p>The user/ password pair is incorrect!</p>"});
            }

            return RedirectToAction("Index", "Login", new {message = GetModelStateErrorsHtmlString()});
        }

        #region PRIVATE Helper Methods

        private RegistrationResultViewModel GetRegistrationResultViewModel(bool identityResult)
        {
            if (identityResult)
            {
                return new RegistrationResultViewModel
                {
                    IsSuccess = true,
                    Message = "<p>Test User registration successfully! User can now log in (<b>Username: admin</b> - <b>Password: f1test2018</b>)!<p>"
                };
            }

            return new RegistrationResultViewModel
            {
                IsSuccess = false,
                Message = "<p>Error creating Test User. The Test User may already exist! Please try logging in or contact IT Operations!<p>"
            };
        }

        private string GetModelStateErrorsHtmlString()
        {
            StringBuilder sb = new StringBuilder();

            foreach(var modelError in ModelState.SelectMany(keyValuePair => keyValuePair.Value.Errors))
            {
                sb.Append($"<p>{modelError.ErrorMessage}</p>");
            }

            return sb.ToString();
        }

        #endregion
    }
}