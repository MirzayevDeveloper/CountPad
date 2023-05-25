// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;
using CountPad.Domain.Common;

namespace CountPad.Domain.Entities.Identities
{
	public class RolePermission : BaseEntity
	{
		public Guid RoleId { get; set; }
		public Role Role { get; set; }

		public Guid PermissionId { get; set; }
		public Permission Permission { get; set; }
	}
}
