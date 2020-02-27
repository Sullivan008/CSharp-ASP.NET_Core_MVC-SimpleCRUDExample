using System;
using System.Threading.Tasks;
using Business.Engine.Interfaces;
using Core.ApplicationCore.BackEndExceptionHandler;
using Data.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace Business.Engine.Engines
{
    public class UserEngine : IUserEngine
    {
        private readonly UserManager<CRUDAppUser> _userManager;

        private readonly SignInManager<CRUDAppUser> _signInManager;

        private readonly IBackEndExceptionHandler _backEndExceptionHandler;

        public UserEngine(UserManager<CRUDAppUser> userManager, SignInManager<CRUDAppUser> signInManager, IBackEndExceptionHandler backedExceptionHandler)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _backEndExceptionHandler =
                backedExceptionHandler ?? throw new ArgumentNullException(nameof(backedExceptionHandler));
        }

        public async Task<bool> Register(string userName, string password)
        {
            CRUDAppUser user = await _userManager.FindByNameAsync(userName);

            try
            {
                if (user != null)
                {
                    return false;
                }

                IdentityResult identityResult = await _userManager.CreateAsync(new CRUDAppUser
                {
                    UserName = userName,
                    Email = $"{userName}@test.com"
                }, password);

                return identityResult.Succeeded;
            }
            catch (Exception ex)
            {
                _backEndExceptionHandler.ExceptionOperations("Error registering User", ex);

                return false;
            }
        }

        public async Task<bool> Login(string userName, string password)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(userName, password, false, false);

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
