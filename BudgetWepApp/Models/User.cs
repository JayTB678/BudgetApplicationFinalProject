namespace BudgetWepApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public double CurrentBalance { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public DateTime AccountCreationDate { get; set; }
    }
}
