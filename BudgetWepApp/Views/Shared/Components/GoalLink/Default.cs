using Microsoft.AspNetCore.Mvc;
using BudgetWepApp.Models;
namespace BudgetWepApp.Views.Shared.Components.GoalLink
{
    public class Default : ViewComponent
    {
        public IViewComponentResult Invoke(string name)
        {
            Goal goal = new Goal
            {
                Name = name,
            };
            return View(goal);
        }

    }
}
