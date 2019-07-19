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
        /// <summary>
        ///     Async Method - Regisztrál (Azaz lement egy User-t) az adatbázisba, a paraméterben megadott
        ///     adatok alapján.
        /// </summary>
        /// <param name="userManager">A felhasználók kezeléséhez szükséges objektum.</param>
        /// <param name="userName">A tárolandó felhasználónév</param>
        /// <param name="password">A tárolandó felhasználónévhez tartozó jelszó.</param>
        /// <returns>TRUE - Ha sikeres volt a "Regisztráció" | FALSE - Ha nem.</returns>
        public async Task<bool?> Register(UserManager<AppUser> userManager, string userName, string password)
        {
            /// Lekérdezzük a a User-t a User Name alapján, hogy megtalálhtó-e az adatbázisban.
            AppUser user = await userManager.FindByNameAsync(userName);

            try
            {
                /// Vizsgálat, hogy az eredmény tartalmaz-e értéket, majd annak megfelelően végzünk műveletet.
                /// NULL - A felhasználó nincs az adatbázisban.
                /// NotNULL = A felhasználó megtalálható az adatbázisban.
                /// Catch - Hiba a felhasználó beszúrása során
                if(user != null)
                {
                    return false;
                }
                else
                {
                    /// A beszúrandó AppUser objektum elkészítése.
                    user = new AppUser
                    {
                        UserName = userName,
                        Email = $"{userName}@test.com"
                    };

                    /// Az elkészített AppUser objektum és a praméterben kapott Password alapján a felhasználó beszúrása
                    /// az adatbázisba. (HASH-elt passworddel)
                    IdentityResult result = await userManager.CreateAsync(user, password);

                    /// A beszúrás eredményének vizsgálata, majd annak megfelelően a művelet végrehajtása.
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

        /// <summary>
        ///     Async Method - Bejelentkeztet egy felhasználót a Bejelentkezetéshez szükséges API-kat tartalmazó objektumokkal..
        /// </summary>
        /// <param name="signInManager">A bejelentkeztetés megvalósításához szükséges objektum.</param>
        /// <param name="userName">A tárolandó felhasználónév</param>
        /// <param name="password">A tárolandó felhasználónévhez tartozó jelszó.</param>
        /// <returns>TRUE - Ha a bejelentkezés sikeresen megvalósult | FALSE - Ha nem.</returns>
        public async Task<bool> Login(SignInManager<AppUser> signInManager, string userName, string password)
        {
            /// A paraméterben megadott Felhasználónév és Jelszó párossal bejelentkeztet egy felhasználót.
            var result = await signInManager.PasswordSignInAsync(userName, password, false, false);

            /// A bejelentkezés eredményének vizsgálata, majd annak megfelelően a művelet végrehajtása.
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///     Async Method - Kiejelentkeztet egy felhasználót a paraméterben átadott szükséges API-kat tartalmazó objektummal..
        /// </summary>
        /// <param name="signInManager">A bejelentkeztetés megvalósításához szükséges objektum.</param>
        public async Task Logout(SignInManager<AppUser> signInManager)
        {
            await signInManager.SignOutAsync();
        }
    }
}
