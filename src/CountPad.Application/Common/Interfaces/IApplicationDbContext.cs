using System.Threading;
using System.Threading.Tasks;
using CountPad.Domain.Entities;
using CountPad.Domain.Entities.Identities;
using CountPad.Domain.Entities.Orders;
using CountPad.Domain.Entities.Packages;
using CountPad.Domain.Entities.Products;
using CountPad.Domain.Entities.Solds;
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
		DbSet<Role> Roles { get; }
		DbSet<Permission> Permissions { get; }
		DbSet<RolePermission> RolePermissions { get; }
		DbSet<UserRole> UserRoles { get; }
		DbSet<Sold> Solds { get; }
		DbSet<User> Users { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
