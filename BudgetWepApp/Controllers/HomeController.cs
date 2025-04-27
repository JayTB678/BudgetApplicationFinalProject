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
            return View();
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
        public IActionResult Goals(Goal goal)
        {
            string userID = userManager.GetUserId(User);
            var goals = context.Goals.Where(g => g.userId == userID).ToList() ?? new List<Goal>();
			return View(goals);
		}

		public IActionResult CreateGoal()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateGoal(Goal goal)
		{
            string userID = userManager.GetUserId(User);

            goal.User = context.Users.FirstOrDefault(u => u.Id == userID);

			if (goal.User == null)
			{
				ModelState.AddModelError("", "User not found.");
			}
			goal.DateAddded = DateTime.Now;
			context.Goals.Add(goal);
			context.SaveChanges();
			return RedirectToAction("Goals", new { userId = goal.userId });
		}
        [HttpGet]
        public IActionResult GoalDetails(int goalId)
        {
            var goal = context.Goals.FirstOrDefault(g => g.GoalID == goalId);
            if (goal == null)
            {
                return NotFound("Goal not found");
            }
            return View(goal);
        }
        [HttpGet]
        public IActionResult EditGoal(int goalId)
        {
            var goal = context.Goals.FirstOrDefault(g => g.GoalID == goalId);
            if (goal == null)
            {
                return NotFound("Goal not found");
            }
            return View(goal);
        }
        [HttpPost]
        public IActionResult EditGoal(Goal goal)
        {
            if (ModelState.IsValid)
            {
                string userID = userManager.GetUserId(User);
                var goalToUpdate = context.Goals.FirstOrDefault(g => g.GoalID == goal.GoalID);
                if (goalToUpdate != null)
                {
                    goalToUpdate.Name = goal.Name;
                    goalToUpdate.Description = goal.Description;
                    goalToUpdate.DateAddded = DateTime.Now;
                    context.SaveChanges();
                    return RedirectToAction("Goals", new { userId = goalToUpdate.userId });
                }
                else
                {
                    return NotFound("Goal not found");
                }
            }
            return View(goal);
        }
        [HttpGet]
        public IActionResult DeleteGoal(int goalId)
        {
           var goal = context.Goals.FirstOrDefault(g => g.GoalID == goalId);
            if (goal == null)
            {
                return NotFound("Goal not found");
            }
            return View(goal);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int goalId)
        {
            var goal = context.Goals.FirstOrDefault(g => g.GoalID == goalId);

            if (goal != null)
            {
                context.Goals.Remove(goal);
                context.SaveChanges();
            }

            return RedirectToAction("Goals");
        }
        [HttpPost]
        public IActionResult UpdateCompletedGoal(int GoalID, bool Completed)
        {
            var goal = context.Goals.FirstOrDefault(g => g.GoalID == GoalID);
            if (goal != null)
            {
                goal.IsCompleted = Completed;
                context.SaveChanges();
            }
            return RedirectToAction("GoalDetails", new {goalId = GoalID });
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
            model.Transactions = context.Transactions.Where(t => t.User.Id == userID).OrderByDescending(t =>t.TransactionID).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddIncome(NewTransationViewModel model)
        {
            if (ModelState.IsValid)
            {

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
            var income = context.Incomes.FirstOrDefault(i => i.IncomeID == incomeId);

            if (income != null)
            {
                context.Incomes.Remove(income);
                context.SaveChanges();
            }

            return RedirectToAction("BankAccountInfo");
        }

        public IActionResult WithdrawalsPage(UserViewModel model)
        {
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
        public IActionResult AddPayment(NewTransationViewModel model)
        {
            if (ModelState.IsValid)
            {
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
                    var newPayment = new RecurringPayment()
                    {
                        User = user,
                        PaymentAmount = model.Amount,
                        PaymenFrequencyDays = model.FrequencyInDays ?? 0,
                        StartingDate = DateTime.Now,
                    };

                    context.recurringPayments.Add(newPayment);
                }

                if (model.UpdateTransactions)
                {
                    var newTransaction = new Transaction()
                    {
                        User = user,
                        TimeStamp = DateTime.Now,
                        Ammount = model.Amount * -1
                    };

                    context.Transactions.Add(newTransaction);

                    user.CurrentBalance -= model.Amount;
                }

                context.SaveChanges();

                return RedirectToAction("WithdrawalsPage");
            }

            return View("WithdrawalsPage");
        }

        [HttpPost]
        public IActionResult RemovePayment(int paymentId)
        {
            var payment = context.recurringPayments.FirstOrDefault(p => p.RecurringPaymentId == paymentId);

            if (payment != null)
            {
                context.recurringPayments.Remove(payment);
                context.SaveChanges();
            }

            return RedirectToAction("WithdrawalsPage");
        }
    }
}
