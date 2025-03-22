using System.Diagnostics;
using System.Security.Principal;
using BudgetWepApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetWepApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int userID = 1;
            var model = new UserViewModel();
            model.User = context.Users.FirstOrDefault(u => u.UserID == userID);
            model.Goals = context.Goals.Where(g => g.User.UserID == userID).ToList();
            model.Income = context.Incomes.Where(i => i.User.UserID == userID).ToList();
            model.RecurringPayments = context.recurringPayments.Where(r => r.User.UserID == userID).ToList();
            model.Transactions = context.Transactions.Where(t => t.User.UserID == userID).OrderByDescending(t => t.TransactionID).ToList();
            return View(model);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Goals()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult BankAccountInfo()
        {
            return View();
        }

        public IActionResult WithdrawalsPage()
        {
            return View();
        }
    }
}
