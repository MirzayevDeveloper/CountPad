using System;
using System.Reflection;
using CountPad.Application.Common.Interfaces;
using CountPad.Domain.Entities;
using CountPad.Domain.Entities.Orders;
using CountPad.Domain.Entities.Packages;
using CountPad.Domain.Entities.Products;
using CountPad.Domain.Entities.Roles;
using CountPad.Domain.Entities.Solds;
using CountPad.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }

		public DbSet<Distributor> Distributors { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Package> Packages { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<RolePermission> RolePermissions { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<Sold> Solds { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(
				assembly: Assembly.GetExecutingAssembly());

			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			{
				modelBuilder.Entity(entity.Name).HasKey("Id");

				modelBuilder.Entity(entity.Name).Property("Id")
					.ValueGeneratedOnAdd();

				modelBuilder.Entity(entity.Name)
					.Property<DateTimeOffset>("CreatedDate")
					.HasDefaultValue(DateTimeOffset.Now);
			}
		}
	}
}
