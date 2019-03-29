
namespace Shop.Web.Data
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using Entities;
	using Microsoft.AspNetCore.Identity;
	using Shop.Web.Helpers;

	public class SeedDb
	{
		private readonly DataContext context;
		private readonly IUserHelper userHelper;
		private Random random;

		public SeedDb(DataContext context, IUserHelper userHelper)
		{
			this.context = context;
			this.userHelper = userHelper;
			this.random = new Random();
		}

		public async Task SeedAsync()
		{
			await this.context.Database.EnsureCreatedAsync();
			await this.userHelper.CheckRoleAsync("Admin");
			await this.userHelper.CheckRoleAsync("Customer");

			var user = await this.userHelper.GetUserByEmailAsync("aaa@gmail.com");
			if (user == null)
			{
				user = new User
				{
					FirstName = "aaa",
					LastName = "aaa",
					Email = "aaa@gmail.com",
					UserName = "aaa@gmail.com"
				};

				var result = await this.userHelper.AddUserAsync(user, "123456");
				if (result != IdentityResult.Success)
				{
					throw new InvalidOperationException("Could not create the user in seeder");
				}
				await this.userHelper.AddUserToRoleAsync(user, "Admin");
			}

			var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
			if (!isInRole)
			{
				await this.userHelper.AddUserToRoleAsync(user, "Admin");
			}

			if (!this.context.Products.Any())
			{
				this.AddProduct("First Product", user);
				this.AddProduct("Second Product", user);
				this.AddProduct("Third Product", user);
				await this.context.SaveChangesAsync();
			}	
			if (!this.context.Products.Any())
			{
				this.AddProduct("iPhone X", user);
				this.AddProduct("Magic mouse", user);
				this.AddProduct("iWatch series 4", user);
				await this.context.SaveChangesAsync();
			}
		}

		private void AddProduct(string name, User user)
		{
			this.context.Products.Add(new Product
			{
				Name = name,
				Price = this.random.Next(1000),
				IsAvailabe = true,
				Stock = this.random.Next(100),
				User = user
			});
		}
	}
}
