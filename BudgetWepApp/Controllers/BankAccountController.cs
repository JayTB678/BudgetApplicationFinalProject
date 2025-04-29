using BudgetWepApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetWepApp.Controllers
{
    public class BankAccountController : Controller
    {
        private IUserContext context;
        private readonly UserManager<User> userManager;
        public BankAccountController(IUserContext ctx, UserManager<User> userManager)
        {
            context = ctx;
            this.userManager = userManager;
        }
        [Authorize]
        [HttpGet]
        public IActionResult BankAccountInfo(UserViewModel model)
        {
            var theme = Request.Cookies["data-bs-theme"];
            ViewBag.Theme = theme;
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(60)
            };
            //this will be got from session later

            string userID = userManager.GetUserId(User);
            //Populates the model
            model.User = context.Users.FirstOrDefault(u => u.Id == userID);

            if (model.User == null)
            {
                return NotFound("User not found");
            }

            model.Goals = context.Goals.Where(g => g.User.Id == userID).ToList();
            model.Income = context.Incomes.Where(i => i.User.Id == userID).ToList();
            model.RecurringPayments = context.recurringPayments.Where(r => r.User.Id == userID).ToList();
            model.Transactions = context.Transactions.Where(t => t.User.Id == userID).OrderByDescending(t => t.TransactionID).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddIncome(NewTransationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var theme = Request.Cookies["data-bs-theme"];
                ViewBag.Theme = theme;
                CookieOptions cookies = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(60)
                };

                string userID = userManager.GetUserId(User);
                User user = context.Users.FirstOrDefault(u => u.Id == userID);


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
                        PayPeriodDays = model.FrequencyInDays ?? 0,
                        StartingDate = DateTime.Now,
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
            var theme = Request.Cookies["data-bs-theme"];
            ViewBag.Theme = theme;
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(60)
            };
            var income = context.Incomes.FirstOrDefault(i => i.IncomeID == incomeId);

            if (income != null)
            {
                context.Incomes.Remove(income);
                context.SaveChanges();
            }

            return RedirectToAction("BankAccountInfo");
        }

    }
}
