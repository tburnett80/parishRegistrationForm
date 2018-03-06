using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ParishForms.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            #if RELEASE
                ViewData["IsLocal"] = "false";
            #else
                ViewData["IsLocal"] = "true";
            #endif

            return View();
        }
    }
}
