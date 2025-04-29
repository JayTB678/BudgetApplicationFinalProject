using Microsoft.EntityFrameworkCore;

namespace BudgetWepApp.Models
{
    public interface IUserContext
    {
        DbSet<Goal> Goals { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Income> Incomes { get; set; }
        DbSet<RecurringPayment> recurringPayments { get; set; }
        DbSet<Transaction> Transactions { get; set; }


        int SaveChanges();
    }
}
