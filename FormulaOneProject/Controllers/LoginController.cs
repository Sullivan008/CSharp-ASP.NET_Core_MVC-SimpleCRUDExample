using Microsoft.AspNetCore.Mvc;

namespace FormulaOneProject.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        ///     Method - A bejelentkeztetési felület betöltésére szolgáló Controller Method.
        /// </summary>
        /// <param name="message">Paraméter, amely tartalmazhat esetleges hibaüzenetet, amely a bejelentkezés során jöhet létre.</param>
        /// <returns>Login/Index View</returns>
        public IActionResult Index(string message)
        {
            /// Vizsgálat, hogy tartalmaz-e a bejelentkezés során létrejött hibaüzenetet.
            /// Ha igen, akkor azt megjelenítjük a felületen.
            if(message == null)
            {
                return View();
            }
            else
            {
                ViewBag.Message = message;

                return View();
            }
        }
    }
}