using Business.Entities.DataBaseEntities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Business.Engine.BusinessEngines.Interfaces
{
    public interface IUserEngine
    {
        Task<bool?> Register(UserManager<AppUser> userManager, string userName, string password);

        Task<bool> Login(SignInManager<AppUser> signInManager, string userName, string password);

        Task Logout(SignInManager<AppUser> signInManager);
    }
}
