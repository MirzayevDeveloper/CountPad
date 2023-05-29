using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CountPad.Domain.Entities;
using CountPad.Domain.Entities.Identities;
using CountPad.Domain.Entities.Orders;
using CountPad.Domain.Entities.Packages;
using CountPad.Domain.Entities.Products;
using CountPad.Domain.Entities.Tokens;
using CountPad.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.Common.Interfaces
{
	public interface IApplicationDbContext
	{
		DbSet<Distributor> Distributors { get; }
		DbSet<Order> Orders { get; }
		DbSet<Package> Packages { get; }
		DbSet<Product> Products { get; }
		DbSet<ProductCategory> ProductCategories { get; }
		DbSet<Role> Roles { get; }
		DbSet<Permission> Permissions { get; }
		DbSet<User> Users { get; }
		DbSet<UserRefreshToken> RefreshTokens { get; }
		DbSet<PackageHistory> PackageHistories { get; }

		IQueryable<T> GetByIds<T>(IEnumerable<Guid> ids) where T : class;

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
