// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;
using System.Collections.Generic;

namespace CountPad.Domain.Entities.Roles
{
	public class Permission
	{
		public Guid Id { get; set; }
		public string PermissionName { get; set; }

		public ICollection<RolePermission> RolePermissions { get; set; }
	}
}
