using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Identity;

namespace BudgetWepApp.Models
{
    public class UserViewModel
    {
        public User User { get; set; }

        public Goal Goal { get; set; }
        public Income Incomes { get; set; }
        public RecurringPayment RecurringPayment { get; set; }
        public Transaction Transaction { get; set; }

        public List<Goal> Goals { get; set; }
        public List<Income> Income { get; set; }
        public List<RecurringPayment> RecurringPayments { get; set; }
        public List<Transaction> Transactions { get; set; }

        public IEnumerable<User> Users { get; set; } = null!;
        public IEnumerable<IdentityRole> Roles { get; set; } = null!;
    }
}
