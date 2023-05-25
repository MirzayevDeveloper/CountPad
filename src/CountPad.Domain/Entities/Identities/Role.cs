// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System.Collections.Generic;
using CountPad.Domain.Common;

namespace CountPad.Domain.Entities.Identities
{
	public class Role : BaseAuditableEntity
	{
		public string RoleName { get; set; }

		public ICollection<UserRole> UserRoles { get; set; }
		public ICollection<RolePermission> RolePermissions { get; set; }
	}
}
