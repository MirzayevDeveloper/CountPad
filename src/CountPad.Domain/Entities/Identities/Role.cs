// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System.Collections.Generic;
using CountPad.Domain.Common.BaseEntities;
using CountPad.Domain.Entities.Users;

namespace CountPad.Domain.Entities.Identities
{
	public class Role : BaseAuditableEntity
	{
		public string RoleName { get; set; }

		public virtual ICollection<User> Users { get; set; }
		public virtual ICollection<Permission> Permissions { get; set; }
	}
}
