using BookstoreApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace BookstoreApplication
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var lib1 = await userManager.FindByNameAsync("john");
            if (lib1 == null)
            {
                lib1 = new ApplicationUser
                {
                    UserName = "john",
                    Email = "john.doe@example.com",
                    Name = "John",
                    Surname = "Doe",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(lib1, "John123!");
            }

            if (!await userManager.IsInRoleAsync(lib1, "Librarian"))
            {
                await userManager.AddToRoleAsync(lib1, "Librarian");
            }

            var editor1 = await userManager.FindByNameAsync("jane");
            if (editor1 == null)
            {
                editor1 = new ApplicationUser
                {
                    UserName = "jane",
                    Email = "jane.doe@example.com",
                    Name = "Jane",
                    Surname = "Doe",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(editor1, "Jane123!");
            }

            if (!await userManager.IsInRoleAsync(editor1, "Editor"))
            {
                await userManager.AddToRoleAsync(editor1, "Editor");
            }
        }
    }
}
