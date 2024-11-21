using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitTrack.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        // Navigation properties
        public virtual ICollection<Workout> Workouts { get; set; } = new List<Workout>();
        public virtual ICollection<Goal> Goals { get; set; } = new List<Goal>();
    }
}