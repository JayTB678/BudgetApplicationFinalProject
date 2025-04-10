﻿using System.ComponentModel.DataAnnotations;

namespace BudgetWepApp.Models
{
    public class Goal
    {
        public int GoalID { get; set; }
        public int UserID { get; set; }
        public User? User { get; set; }
        [Required(ErrorMessage = "Please enter a name for the goal.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter a description for the goal.")]
        public string? Description { get; set; }
        public DateTime DateAddded { get; set; }
    }
}
