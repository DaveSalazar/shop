using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data
{
	public class DataContext : IdentityDbContext<User>
	{
		public DbSet<Product> Products { get; set; }
		public DbSet<Country> Countries { get; set; }
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Product>()
			.Property(p => p.Price)
			.HasColumnType("decimal(18,2)");

			var cascadeFKs = builder.Model
				.G­etEntityTypes()
				.SelectMany(t => t.GetForeignKeys())
				.Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Casca­de);
			foreach (var fk in cascadeFKs)
			{
				fk.DeleteBehavior = DeleteBehavior.Restr­ict;
			}

			base.OnModelCreating(builder);
		}
	}
}
