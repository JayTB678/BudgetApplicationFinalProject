using BudgetWepApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetWepApp.Controllers
{
    public class GoalController : Controller
    {
        private IUserContext context;
        private readonly UserManager<User> userManager;
        public GoalController(IUserContext ctx, UserManager<User> userManager)
        {
            context = ctx;
            this.userManager = userManager;
        }
        public IActionResult Goals()
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
            return RedirectToAction("GoalDetails", new { goalId = GoalID });
        }
    }
}
