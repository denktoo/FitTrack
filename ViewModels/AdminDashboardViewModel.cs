namespace FitTrack.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalWorkouts { get; set; }
        public int GoalsAchieved { get; set; }
        public int PendingGoals { get; set; }
        public List<UserOverview> Users { get; set; }
        public List<WorkoutOverview> RecentWorkouts { get; set; }
        public List<GoalOverview> RecentGoals { get; set; }
    }

    public class UserOverview
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public int WorkoutsCount { get; set; }
        public int GoalsAchievedCount { get; set; }
    }

    public class WorkoutOverview
    {
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
    }

    public class GoalOverview
    {
        public string UserName { get; set; }
        public string Description { get; set; }
        public DateTime TargetDate { get; set; }
    }
}
