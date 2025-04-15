using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetWepApp.Models
{
    public class User : IdentityUser
    {
        public double CurrentBalance { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime AccountCreationDate { get; set; }

        public virtual ICollection<Goal>? Goals { get; set; }
        public virtual ICollection<Income>? Incomes { get; set; }
        public virtual ICollection<Transaction>? Transactions { get; set; }
        public virtual ICollection<RecurringPayment>? RecurringPayments { get; set; }
    }
}
