using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace BudgetWepApp.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options){ }

        public DbSet<Goal> Goals { get; set; } = null!;
        public DbSet<Income> Incomes { get; set; } = null!;
        public DbSet<RecurringPayment> recurringPayments { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Goal>().HasData(

                new
                { 
                    GoalID = 1,
                    UserID = 1,
                    Name = "Goal 1",
                    Description = "This is the first goal.",
                    DateAddded = new DateTime(2025, 3, 3)
                },
                new
                { 
                    GoalID = 2,
                    UserID = 2,
                    Name = "Goal 2",
                    Description = "This is the second goal.",
                    DateAddded = new DateTime(2020, 3, 3)
                }
            );

            modelBuilder.Entity<Income>().HasData(

                new
                {
                    IncomeID = 1,
                    UserID = 1,
                    IncomeAmmount = 100.00,
                    PayPeriodDays = 14,
                    DaysTillNextPayment = 5
                },
                new
                {
                    IncomeID = 2,
                    UserID = 2,
                    IncomeAmmount = 200.00,
                    PayPeriodDays = 7,
                    DaysTillNextPayment = 5
                }
            );

            modelBuilder.Entity<RecurringPayment>().HasData(
                
                new
                {
                    RecurringPaymentId = 1,
                    UserID = 1,
                    PaymentAmount = 100.00,
                    PaymenFrequencyDays = 14,
                    DaysTillNextPayment = 5
                },
                new
                {
                    RecurringPaymentId = 2,
                    UserID = 2,
                    PaymentAmount = 200.00,
                    PaymenFrequencyDays = 7,
                    DaysTillNextPayment = 5
                }
            );

            modelBuilder.Entity<Transaction>().HasData(

                new
                {
                    TransactionID = 1,
                    UserID = 1,
                    TimeStamp = new DateTime(2025, 3, 3),
                    Ammount = 100.00
                },
                new
                {
                    TransactionID = 2,
                    UserID = 1,
                    TimeStamp = new DateTime(2020, 3, 3),
                    Ammount = -50.00
                },
                new
                {
                    TransactionID = 3,
                    UserID = 2,
                    TimeStamp = new DateTime(2020, 3, 3),
                    Ammount = 200.00
                }
            );

            modelBuilder.Entity<User>().HasData(
                new
                {
                    UserID = 1,
                    UserName = "Name1",
                    Password = "1234",
                    CurrentBalance = 1000.00,
                    IsAdmin = true,
                    Email = "test1@email.com",
                    AccountCreationDate = new DateTime(2025, 3, 3)
                },
                new
                {
                    UserID = 2,
                    UserName = "Name2",
                    Password = "1234",
                    CurrentBalance = 2000.00,
                    IsAdmin = true,
                    Email = "test2@email.com",
                    AccountCreationDate = new DateTime(2020, 3, 3)
                }
            );
        }
    }
}
