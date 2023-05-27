// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System.Collections.Generic;
using CountPad.Domain.Common.BaseEntities;

namespace CountPad.Domain.Entities.Identities
{
	public class Permission : BaseAuditableEntity
	{
		public string PermissionName { get; set; }

		public virtual ICollection<Role> Roles { get; set; }
	}
}
