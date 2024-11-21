using System.ComponentModel.DataAnnotations;

namespace FitTrack.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}