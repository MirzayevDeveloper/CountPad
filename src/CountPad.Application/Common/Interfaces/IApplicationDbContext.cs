using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
		DbSet<Sold> Solds { get; }
		DbSet<User> Users { get; }

		IQueryable<T> GetByIds<T>(IEnumerable<Guid> ids) where T: class;

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
