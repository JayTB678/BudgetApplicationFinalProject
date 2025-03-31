using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BudgetWepApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter username")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; } = string.Empty ;
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; } = string.Empty;
        public string returnUrl { get; set; } = string.Empty;
        public bool rememberMe { get; set; }
    }
}
