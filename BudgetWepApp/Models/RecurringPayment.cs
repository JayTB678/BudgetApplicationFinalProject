namespace BudgetWepApp.Models
{
    public class RecurringPayment
    {
        public int RecurringPaymentId { get; set; }
        public string userId { get; set; }
        public User User { get; set; }
        public double PaymentAmount { get; set; }
        public int PaymenFrequencyDays { get; set; }
        public DateTime StartingDate { get; set; }
    }
}
