using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BudgetWepApp.Models
{
    public class UserContext :IdentityDbContext<User>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options){ }

        public DbSet<Goal> Goals { get; set; } = null!;
        public DbSet<Income> Incomes { get; set; } = null!;
        public DbSet<RecurringPayment> recurringPayments { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Goal>()
            .HasOne(g => g.User)
            .WithMany(u => u.Goals)
            .HasForeignKey(g => g.userId);

            modelBuilder.Entity<Income>()
                .HasOne(i => i.User)
                .WithMany(u => u.Incomes)
                .HasForeignKey(i => i.userId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.userId);

            modelBuilder.Entity<RecurringPayment>()
                .HasOne(r => r.User)
                .WithMany(u => u.RecurringPayments)
                .HasForeignKey(r => r.userId);

            modelBuilder.Entity<Goal>().HasData(

                new
                { 
                    GoalID = 1,
                    userId = "1",
                    Name = "Goal 1",
                    Description = "This is the first goal.",
                    DateAddded = new DateTime(2025, 3, 3)
                },
                new
                { 
                    GoalID = 2,
                    userId = "2",
                    Name = "Goal 2",
                    Description = "This is the second goal.",
                    DateAddded = new DateTime(2020, 3, 3)
                }
            );

            modelBuilder.Entity<Income>().HasData(

                new
                {
                    IncomeID = 1,
                    userId = "1",
                    IncomeAmmount = 100.00,
                    PayPeriodDays = 14,
                    DaysTillNextPayment = 5
                },
                new
                {
                    IncomeID = 2,
                    userId = "2",
                    IncomeAmmount = 200.00,
                    PayPeriodDays = 7,
                    DaysTillNextPayment = 5
                }
            );

            modelBuilder.Entity<RecurringPayment>().HasData(
                
                new
                {
                    RecurringPaymentId = 1,
                    userId = "1",
                    PaymentAmount = 100.00,
                    PaymenFrequencyDays = 14,
                    DaysTillNextPayment = 5
                },
                new
                {
                    RecurringPaymentId = 2,
                    userId = "2",
                    PaymentAmount = 200.00,
                    PaymenFrequencyDays = 7,
                    DaysTillNextPayment = 5
                }
            );

            modelBuilder.Entity<Transaction>().HasData(

                new
                {
                    TransactionID = 1,
                    userId = "1",
                    TimeStamp = new DateTime(2025, 3, 3),
                    Ammount = 100.00
                },
                new
                {
                    TransactionID = 2,
                    userId = "1",
                    TimeStamp = new DateTime(2020, 3, 3),
                    Ammount = -50.00
                },
                new
                {
                    TransactionID = 3,
                    userId = "2",
                    TimeStamp = new DateTime(2020, 3, 3),
                    Ammount = 200.00
                }
            );
            modelBuilder.Entity<User>().HasData(
       new User
       {
           Id = "1",
           UserName = "Name1",
           NormalizedUserName = "NAME1",
           Email = "test1@email.com",
           NormalizedEmail = "TEST1@EMAIL.COM",
           EmailConfirmed = true,
           PasswordHash = "password",
           SecurityStamp = "SECURITYSTAMP1",
           ConcurrencyStamp = "CONCURRENCYSTAMP1",
           CurrentBalance = 1000.0,
           IsAdmin = true,
           AccountCreationDate = new DateTime(2025, 3, 3)
       },
        new User
        {
            Id = "2",
            UserName = "Name2",
            NormalizedUserName = "NAME2",
            Email = "test2@email.com",
            NormalizedEmail = "TEST2@EMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = "password2",
            SecurityStamp = "SECURITYSTAMP2",
            ConcurrencyStamp = "CONCURRENCYSTAMP2",
            CurrentBalance = 2000.0,
            IsAdmin = true,
            AccountCreationDate = new DateTime(2020, 3, 3)
        }
       );
        }
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            using (var scoped = serviceProvider.CreateScope())
            {
                UserManager<User> userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();
                RoleManager<IdentityRole> roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string username = "admin";
                string pwd = "admin";
                string roleName = "Admin";

                // if role doesn't exist, create it
                if (await roleManager.FindByNameAsync(roleName) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                if (await userManager.FindByNameAsync(username) == null)
                {
                    User user = new User() { UserName = username };
                    var result = await userManager.CreateAsync(user, pwd);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }
                }
            }
        }
    }

}
