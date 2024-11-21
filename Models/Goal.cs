using System;
using System.ComponentModel.DataAnnotations;

namespace FitTrack.Models
{
    public class Goal
    {
        public int Id { get; set; }

        public int UserId { get; set; } // Foreign key to User

        public int WorkOutId { get; set; } // Foreign key to Workout

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public DateTime TargetDate { get; set; }

        public bool IsAchieved { get; set; } // Indicates if the goal has been achieved
    }
}