using Microsoft.AspNetCore.Mvc;

namespace FormulaOneProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(string message)
        {
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