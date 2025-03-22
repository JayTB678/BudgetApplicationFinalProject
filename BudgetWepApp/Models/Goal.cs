namespace BudgetWepApp.Models
{
    public class Goal
    {
        public int GoalID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateAddded { get; set; }
    }
}
