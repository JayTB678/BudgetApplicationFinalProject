namespace BudgetWepApp.Models
{
    public class NewTransationViewModel
    {
        public double Amount { get; set; }

        //leave null if its not reacuring
        public int? FrequencyInDays { get; set; }

        public bool UpdateTransactions { get; set; }
    }
}
