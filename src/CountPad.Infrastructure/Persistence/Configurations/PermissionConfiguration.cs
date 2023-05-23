using CountPad.Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountPad.Infrastructure.Persistence.Configurations
{
	public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
	{
		public void Configure(EntityTypeBuilder<Permission> builder)
		{
			builder.HasIndex(x => x.PermissionName)
				.IsUnique();
		}
	}
}
