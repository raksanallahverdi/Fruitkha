using Common.Constants;
using Common.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Data
{
	public class DbInitializer
	{
		public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			await AddRolesAsync(roleManager);
			await AddAdminAsync(userManager, roleManager);
		}

		private static async Task AddRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			foreach (var role in Enum.GetValues<UserRoles>())
			{
				if (!await roleManager.RoleExistsAsync(role.ToString()))
				{
					var result = await roleManager.CreateAsync(new IdentityRole
					{
						Name = role.ToString()
					});

					if (!result.Succeeded)
					{
						throw new Exception($"Failed to create role: {role}");
					}
				}
			}
		}

		private static async Task AddAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			if (await userManager.FindByEmailAsync("admin@app.com") == null)
			{
				var user = new User
				{
					Email = "admin@app.com",
					UserName = "admin@app.com"
				};

				var createUserResult = await userManager.CreateAsync(user, "Admin123!");
				if (!createUserResult.Succeeded)
				{
					throw new Exception("Couldn't add Admin. Errors: " + string.Join(", ", createUserResult.Errors));
				}

				var role = await roleManager.FindByNameAsync("Admin");
				if (role == null)
				{
					throw new Exception("The Role 'Admin' was not found!");
				}

				var addRoleResult = await userManager.AddToRoleAsync(user, role.Name);
				if (!addRoleResult.Succeeded)
				{
					throw new Exception("Couldn't add Admin role to User. Errors: " + string.Join(", ", addRoleResult.Errors));
				}
			}
		}
	}
}
