using System.Diagnostics;
using System.Security.Principal;
using BudgetWepApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BudgetWepApp.Controllers
{
    public class HomeController : Controller
    {
        private UserContext context;

        public HomeController(UserContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpGet]
        public IActionResult BankAccountInfo(UserViewModel model)
        {
            //this will be got from session later
            int userID = 1;

            //Populates the model
            model.User = context.Users.FirstOrDefault(u => u.UserID == userID);
            model.Goals = context.Goals.Where(g => g.User.UserID == userID).ToList();
            model.Income = context.Incomes.Where(i => i.User.UserID == userID).ToList();
            model.RecurringPayments = context.recurringPayments.Where(r => r.User.UserID == userID).ToList();
            model.Transactions = context.Transactions.Where(t => t.User.UserID == userID).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddIncome(Income income)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserID = 1;

                

                return RedirectToAction("BankAccountInfo");
            }

            return View("BankAccountInfo");
        }

        public IActionResult WithdrawalsPage()
        {
            return View();
        }
    }
}
