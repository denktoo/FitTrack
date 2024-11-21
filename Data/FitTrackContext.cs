using Microsoft.EntityFrameworkCore;
using FitTrack.Models;
using System.Numerics;
using System.Diagnostics;

namespace FitTrack.Data
{
    public class FitTrackContext : DbContext
    {
        public FitTrackContext(DbContextOptions<FitTrackContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User to Workout relationship
            modelBuilder.Entity<Workout>()
                .HasOne<User>()
                .WithMany(p => p.Workouts)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User to Goal relationship
            modelBuilder.Entity<Goal>()
                .HasOne<User>()
                .WithMany(p => p.Goals)
                .HasForeignKey(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Workout referencing Goal relationship
            modelBuilder.Entity<Goal>()
                .HasOne<Workout>()
                .WithMany(s => s.Goals)
                .HasForeignKey(g => g.WorkOutId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data and Explicitly declare Role shadow property and set Role for the admin user
            modelBuilder.Entity<User>().Property<string>("Role");
            modelBuilder.Entity<User>().HasData(
                new { Id = 1, Username = "admin", Email = "admin@gmail.com", Password = "admin123", Role = "Admin" }
            );
        }
    }
}