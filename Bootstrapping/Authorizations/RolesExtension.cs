using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TestAspCoreTuto.Authorizations
{
    public static class RolesExtension
    {
        public static void AddRoles(this IServiceProvider services)
        {
            CreateRoles(services).Wait();
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<IdentityUser> UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityResult roleResult;
            //here in this line we are adding Admin Role
            bool roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //here in this line we are creating admin role and seed it to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            //here we are assigning the Admin role to the User that we have registered above 
            //Now, we are assinging admin role to this user("Ali@gmail.com"). When will we run this project then it will
            //be assigned to that user.
            IdentityUser user = await UserManager.FindByEmailAsync("choko@gmail.com");
            IdentityUser User = new IdentityUser();
            await UserManager.AddToRoleAsync(user, "Admin");
        }
    }
}
