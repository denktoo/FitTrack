using System.Collections.Generic;
using FitTrack.Models;

namespace FitTrack.ViewModels
{
    public class UserDashboardViewModel
    {
        public IEnumerable<Workout> Workouts { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
    }
}