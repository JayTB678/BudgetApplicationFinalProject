using System.Diagnostics;
using System.Security.Principal;
using BudgetWepApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BudgetWepApp.Controllers
{
    public class HomeController : Controller
    {
        private UserContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public HomeController(UserContext ctx, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            context = ctx;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (!signInManager.IsSignedIn(User))
            {
                return View();
            }
            else
            {
                var theme = Request.Cookies["data-bs-theme"];
                ViewBag.Theme = theme;
                CookieOptions cookies = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(60)
                };
                string userID = userManager.GetUserId(User);
                //string userID = "1";
                var model = new UserViewModel();
                model.User = context.Users.FirstOrDefault(u => u.Id == userID);
                model.Goals = context.Goals.Where(g => g.User.Id == userID).ToList();
                model.Income = context.Incomes.Where(i => i.User.Id == userID).ToList();
                model.RecurringPayments = context.recurringPayments.Where(r => r.User.Id == userID).ToList();
                model.Transactions = context.Transactions.Where(t => t.User.Id == userID).OrderByDescending(t => t.TransactionID).ToList();
                return View(model);
            }
        }
        
        public IActionResult Contact()
        {
            var theme = Request.Cookies["data-bs-theme"];
            ViewBag.Theme = theme;
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(60)
            };
            return View();
        }
        
        public IActionResult Thanks()
        {
            var theme = Request.Cookies["data-bs-theme"];
            ViewBag.Theme = theme;
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(60)
            };
            return View("ThankYou");
        }

        [HttpPost]
        public IActionResult SetTheme()
        {
            var currentTheme = Request.Cookies["data-bs-theme"] ?? "light";
            var newTheme = currentTheme == "light" ? "dark" : "light";

            Response.Cookies.Append("data-bs-theme", newTheme, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                HttpOnly = false,
            });

            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Privacy()
        {
            var theme = Request.Cookies["data-bs-theme"];
            ViewBag.Theme = theme;
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(60)
            };
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var theme = Request.Cookies["data-bs-theme"];
            ViewBag.Theme = theme;
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(60)
            };
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
