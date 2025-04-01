using System.ComponentModel.DataAnnotations;

namespace BudgetWepApp.Models
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Please enter username")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter phone number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
