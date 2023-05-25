using System.Linq;
using CountPad.Application.Common.Interfaces;
using CountPad.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CountPad.Infrastructure.Persistence.Interceptors
{
	public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
	{
		private readonly IDateTime _dateTime;
		private readonly IGuidGenerator _guidGenerator;

		public AuditableEntitySaveChangesInterceptor(
			IDateTime dateTime,
			IGuidGenerator guidGenerator)
		{
			_dateTime = dateTime;
			_guidGenerator = guidGenerator;
		}

		public void UpdateEntities(DbContext context)
		{
			if (context == null) return;

			foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
			{
				if (entry.State is EntityState.Added)
				{
					entry.Entity.CreatedDate = _dateTime.Now;
					entry.Entity.Id = _guidGenerator.Guid;
				}

				if (entry.State is EntityState.Added || entry.State is EntityState.Modified || entry.HasChangedOwnedEntities())
				{
					entry.Entity.UpdateDate = _dateTime.Now;
				}
			}
		}
	}

	public static class Extensions
	{
		public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
			entry.References.Any(r =>
				r.TargetEntry != null &&
				r.TargetEntry.Metadata.IsOwned() &&
				(r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
	}
}
