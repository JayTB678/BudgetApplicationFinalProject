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
            model.Transactions = context.Transactions.Where(t => t.User.UserID == userID).OrderByDescending(t =>t.TransactionID).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddIncome(NewTransationViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = context.Users.FirstOrDefault(u => u.UserID == 1);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                if (model.Amount <= 0)
                {
                    return BadRequest("Number not positive");
                }

                if (model.FrequencyInDays > 0)
                {
                    var newIncome = new Income()
                    {
                        User = user,
                        IncomeAmmount = model.Amount,
                        PayPeriodDays = model.FrequencyInDays ?? 0
                    };

                    context.Incomes.Add(newIncome);
                }

                if (model.UpdateTransactions)
                {
                    var newTransaction = new Transaction()
                    {
                        User = user,
                        TimeStamp = DateTime.Now,
                        Ammount = model.Amount
                    };

                    context.Transactions.Add(newTransaction);

                    user.CurrentBalance += model.Amount;
                }

                context.SaveChanges();

                return RedirectToAction("BankAccountInfo");
            }

            return View("BankAccountInfo");
        }

        [HttpPost]
        public IActionResult RemoveIncome(int incomeId)
        {
            var income = context.Incomes.FirstOrDefault(i => i.IncomeID == incomeId);

            if (income != null)
            {
                context.Incomes.Remove(income);
                context.SaveChanges();
            }

            return RedirectToAction("BankAccountInfo");
        }

        public IActionResult WithdrawalsPage()
        {
            return View();
        }
    }
}
