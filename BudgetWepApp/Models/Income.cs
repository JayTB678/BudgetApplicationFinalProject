namespace BudgetWepApp.Models
{
    public class Income
    {
        public int IncomeID { get; set; }
        public string userId { get; set; }
        public User User { get; set; }
        public double IncomeAmmount { get; set; }
        public int PayPeriodDays { get; set; }
        public int DaysTillNextPayment { get; set; }
    }
}
