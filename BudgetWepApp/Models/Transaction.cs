namespace BudgetWepApp.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public string userId { get; set; }
        public User User { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Ammount { get; set; }
    }
}
