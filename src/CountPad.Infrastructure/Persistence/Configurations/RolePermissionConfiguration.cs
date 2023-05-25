using CountPad.Domain.Entities.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountPad.Infrastructure.Persistence.Configurations
{
	public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
	{
		public void Configure(EntityTypeBuilder<RolePermission> builder)
		{
			builder.HasKey(ur => new
			{
				ur.RoleId,
				ur.PermissionId
			});

			builder.HasOne(x => x.Role)
				.WithMany(x => x.RolePermissions)
				.HasForeignKey(x => x.RoleId)
				.IsRequired();

			builder.HasOne(x => x.Permission)
				.WithMany(x => x.RolePermissions)
				.HasForeignKey(x => x.PermissionId)
				.IsRequired();
		}
	}
}
