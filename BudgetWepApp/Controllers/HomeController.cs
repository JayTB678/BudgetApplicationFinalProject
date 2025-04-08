using System.Diagnostics;
using System.Security.Principal;
using BudgetWepApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            int userId = 1;
            var goals = context.Goals.Where(g => g.UserID == userId).ToList();
            return View(goals);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Goals(Goal goal)
        {
            int userId = 1;
			var goals = context.Goals.Where(g => g.UserID == userId).ToList() ?? new List<Goal>();
			return View(goals);
		}

		public IActionResult CreateGoal()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateGoal(Goal goal)
		{
			if (goal.UserID == 0)
			{
				goal.UserID = 1;
			}

			goal.User = context.Users.FirstOrDefault(u => u.UserID == goal.UserID);

			if (goal.User == null)
			{
				ModelState.AddModelError("", "User not found.");
			}
			goal.DateAddded = DateTime.Now;
			context.Goals.Add(goal);
			context.SaveChanges();
			return RedirectToAction("Goals", new { userId = goal.UserID });
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
                var goalToUpdate = context.Goals.FirstOrDefault(g => g.GoalID == goal.GoalID);
                if (goalToUpdate != null)
                {
                    goalToUpdate.Name = goal.Name;
                    goalToUpdate.Description = goal.Description;
                    goalToUpdate.DateAddded = DateTime.Now;
                    context.SaveChanges();
                    return RedirectToAction("Goals", new { userId = goalToUpdate.UserID });
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

            if (model.User == null)
            {
                return NotFound("User not found");
            }

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
                        PayPeriodDays = model.FrequencyInDays ?? 0,
                        DaysTillNextPayment = model.FrequencyInDays ?? 0
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
