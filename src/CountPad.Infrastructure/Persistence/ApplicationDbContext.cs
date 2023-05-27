using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CountPad.Application.Common.Interfaces;
using CountPad.Domain.Entities;
using CountPad.Domain.Entities.Identities;
using CountPad.Domain.Entities.Orders;
using CountPad.Domain.Entities.Packages;
using CountPad.Domain.Entities.Products;
using CountPad.Domain.Entities.Solds;
using CountPad.Domain.Entities.Tokens;
using CountPad.Domain.Entities.Users;
using CountPad.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		private readonly DbContextOptions<ApplicationDbContext> _options;
		private readonly AuditableEntitySaveChangesInterceptor _interceptor;

		public ApplicationDbContext(
			DbContextOptions<ApplicationDbContext> options,
			AuditableEntitySaveChangesInterceptor interceptor)
			: base(options)
		{
			_options = options;
			_interceptor = interceptor;
		}

		public DbSet<Distributor> Distributors { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Package> Packages { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<Sold> Solds { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserRefreshToken> RefreshTokens { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(
				Assembly.GetExecutingAssembly());

			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			{
				modelBuilder.Entity(entity.Name).Property(typeof(DateTimeOffset), "CreatedDate")
					.HasColumnType("timestamptz");

				modelBuilder.Entity(entity.Name).Property(typeof(DateTimeOffset), "UpdatedDate")
					.HasColumnType("timestamptz");
			}

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.AddInterceptors(_interceptor);
		}

		public IQueryable<T> GetByIds<T>(IEnumerable<Guid> ids) where T : class
		{
			var entities = new List<T>();

			using (var context = new ApplicationDbContext(_options, _interceptor))
			{
				foreach (var id in ids)
				{
					entities.Add(context.Find<T>(
						new object[] { id }));
				}
			}

			return entities.AsQueryable();
		}
	}
}
