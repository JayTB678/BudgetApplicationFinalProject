using Microsoft.AspNetCore.Mvc;

namespace BudgetWepApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View("Error");
        }
    }
}
