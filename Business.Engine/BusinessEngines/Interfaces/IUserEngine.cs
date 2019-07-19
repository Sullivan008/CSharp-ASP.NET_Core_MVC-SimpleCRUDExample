using Business.Entities.DataBaseEntities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Business.Engine.BusinessEngines.Interfaces
{
    public interface IUserEngine
    {
        /// <summary>
        ///     Regisztrál (Azaz lement egy User-t) az adatbázisba, a paraméterben megadott
        ///     adatok alapján.
        /// </summary>
        /// <param name="userManager">A felhasználók kezeléséhez szükséges objektum.</param>
        /// <param name="userName">A tárolandó felhasználónév</param>
        /// <param name="password">A tárolandó felhasználónévhez tartozó jelszó.</param>
        /// <returns>TRUE - Ha sikeres volt a "Regisztráció" | FALSE - Ha nem.</returns>
        Task<bool?> Register(UserManager<AppUser> userManager, string userName, string password);

        /// <summary>
        ///     Async Method - Bejelentkeztet egy felhasználót a Bejelentkezetéshez szükséges API-kat tartalmazó objektumokkal..
        /// </summary>
        /// <param name="signInManager">A bejelentkeztetés megvalósításához szükséges objektum.</param>
        /// <param name="userName">A tárolandó felhasználónév</param>
        /// <param name="password">A tárolandó felhasználónévhez tartozó jelszó.</param>
        /// <returns>TRUE - Ha a bejelentkezés sikeresen megvalósult | FALSE - Ha nem.</returns>
        Task<bool> Login(SignInManager<AppUser> signInManager, string userName, string password);

        /// <summary>
        ///     Async Method - Kiejelentkeztet egy felhasználót a paraméterben átadott szükséges API-kat tartalmazó objektummal..
        /// </summary>
        /// <param name="signInManager">A bejelentkeztetés megvalósításához szükséges objektum.</param>
        Task Logout(SignInManager<AppUser> signInManager);
    }
}
