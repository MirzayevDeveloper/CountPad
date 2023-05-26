// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System.Collections.Generic;
using CountPad.Domain.Common;
using CountPad.Domain.Entities.Users;

namespace CountPad.Domain.Entities.Identities
{
	public class Role : BaseAuditableEntity
	{
		public string RoleName { get; set; }

		public ICollection<User> UserRoles { get; set; }
		public ICollection<Permission> RolePermissions { get; set; }
	}
}
