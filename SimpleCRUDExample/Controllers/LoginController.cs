using Microsoft.AspNetCore.Mvc;

namespace SimpleCRUDExample.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(string message)
        {
            if (message == null)
            {
                return View();
            }

            ViewBag.Message = message;
            return View();
        }
    }
}