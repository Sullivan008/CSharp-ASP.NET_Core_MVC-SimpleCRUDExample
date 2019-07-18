using Business.Engine.BusinessEngines.Interfaces;
using Business.Entities.DataBaseEntities;
using Core.Handlers.BackEndExceptionHandler;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Business.Engine.BusinessEngines.Engines
{
    public class UserEngine : IUserEngine
    {
        public async Task<bool?> Register(UserManager<AppUser> userManager, string userName, string password)
        {
            AppUser user = await userManager.FindByNameAsync(userName);

            try
            {
                if(user != null)
                {
                    return false;
                }
                else
                {
                    user = new AppUser();
                    user.UserName = userName;
                    user.Email = $"{userName}@test.com";

                    IdentityResult result = await userManager.CreateAsync(user, password);

                    if(result.Succeeded)
                    {
                        return true;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {
                new BackEndException<Exception>(ex).
                    ExceptionOperations("Hiba a felhasználó regisztrálása során!");

                return null;
            }
        }

        public async Task<bool> Login(SignInManager<AppUser> signInManager, string userName, string password)
        {
            var result = await signInManager.PasswordSignInAsync(userName, password, false, false);

            if(result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task Logout(SignInManager<AppUser> signInManager)
        {
            await signInManager.SignOutAsync();
        }
    }

}
