using System;
using System.ComponentModel.DataAnnotations;

namespace FitTrack.Models
{
    public class Workout
    {
        public int Id { get; set; }

        public int UserId { get; set; } // Foreign key to User

        [Required]
        public DateTime WorkoutDate { get; set; }

        [Required]
        public int Duration { get; set; } // Duration in minutes

        // Navigation properties
        public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
    }
}