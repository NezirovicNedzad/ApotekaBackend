using ApotekaBackend.Data;
using ApotekaBackend.Models;

using Microsoft.AspNetCore.Identity;

namespace FitnessBackend
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            //Kreiramo role i dodajemo admina

            var roles = new List<AppRole>
            {

                new() {Name="Admin"},
                new() {Name="Farmaceut"},
                new(){Name="Apotekar"}
            };


            foreach (var role in roles)
            {
                if (string.IsNullOrWhiteSpace(role.Name))
                {
                    continue; // Skip if the role name is null or empty
                }
                if (!await roleManager.RoleExistsAsync(role.Name))
                    await roleManager.CreateAsync(role);
            }


            var adminEmail = "john@example.com"; // Replace with your desired admin email
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            

            if (adminUser == null) // If the user doesn't exist, create it
            {
                var newAdminUser = new AppUser
                {
                    UserName = "JohnyBoy",
                    Email = adminEmail,
                    Phone="06412313",
                    Name="John",
                    Surname="Kennedy",
                    EmailConfirmed = true // Set tco true if email confirmation isn't required for admin
                };
                
                var result = await userManager.CreateAsync(newAdminUser, "John@123"); // Set a strong password
                Console.WriteLine(result);
                if (result.Succeeded)
                {
                    // Assign the user to the "Admin" role
                    await userManager.AddToRoleAsync(newAdminUser, "Farmaceut");
                }
               
             
            }
        }
    }
}
