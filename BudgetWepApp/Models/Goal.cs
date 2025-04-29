using System.ComponentModel.DataAnnotations;

namespace BudgetWepApp.Models
{
    public class Goal
    {
        public int GoalID { get; set; }
        public string userId { get; set; }
        public User? User { get; set; }
        [Required(ErrorMessage = "A name for your goal is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "A description of your goal is required.")]
        public string Description { get; set; }
        public DateTime DateAddded { get; set; }
        public bool IsCompleted { get; set; }
    }
}
