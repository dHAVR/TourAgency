using Microsoft.AspNetCore.Identity;
using TourAgency.Models;

namespace TourAgency.Data
{
    public static class SeedData
    {
        public static async Task Initialize(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
        
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }


            var adminEmail = "admin@gmail.com";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
      
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "Admin",
                    Surname = "Admin",
                    DateOfBirth = new DateTime(2005, 1, 1)
                };

   
                var result = await userManager.CreateAsync(admin, "AdminPassword123!");

                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(admin, "Admin");
                }
                else
                {
           
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating admin: {error.Description}");
                    }
                }
            }
            else
            {

                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
