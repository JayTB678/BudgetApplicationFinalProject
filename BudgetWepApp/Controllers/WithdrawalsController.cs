using BudgetWepApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetWepApp.Controllers
{
    public class WithdrawalsController : Controller
    {
        private IUserContext context;
        private readonly UserManager<User> userManager;
        public WithdrawalsController(IUserContext ctx, UserManager<User> userManager)
        {
            context = ctx;
            this.userManager = userManager;
        }

        [Authorize]
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