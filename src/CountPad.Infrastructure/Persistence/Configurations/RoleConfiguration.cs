using CountPad.Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountPad.Infrastructure.Persistence.EntityTypeConfigurations
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.HasIndex(r => r.RoleName)
				.IsUnique();
		}
	}
}
