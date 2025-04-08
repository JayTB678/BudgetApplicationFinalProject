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
    }
}
