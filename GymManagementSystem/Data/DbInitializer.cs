using GymManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace GymManagementSystem.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            string[] roles = { "Admin", "Trainer" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            if (await userManager.FindByEmailAsync("admin@gym.com") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@gym.com",
                    Email = "admin@gym.com",
                    FullName = "System Admin",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, "Admin123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

            if (!context.Trainers.Any())
            {
                var trainer1 = new Trainer { Name = "Ahmed Ali", Specialization = "Cardio" };
                var trainer2 = new Trainer { Name = "Sara Youssef", Specialization = "Yoga" };
                context.Trainers.AddRange(trainer1, trainer2);
                context.SaveChanges();

                context.GymClasses.AddRange(
                    new GymClass { Name = "Morning Cardio", Description = "High intensity cardio session", Schedule = "Mon 8:00 AM", TrainerId = trainer1.Id },
                    new GymClass { Name = "Evening Yoga", Description = "Relaxing yoga session", Schedule = "Wed 6:00 PM", TrainerId = trainer2.Id }
                );
                context.SaveChanges();
            }

            if (await userManager.FindByEmailAsync("trainer@gym.com") == null)
            {
                var trainerUser = new ApplicationUser
                {
                    UserName = "trainer@gym.com",
                    Email = "trainer@gym.com",
                    FullName = "Ahmed Ali",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(trainerUser, "Trainer123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(trainerUser, "Trainer");
            }
        }
    }
}