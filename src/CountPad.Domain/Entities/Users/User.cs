// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System.Collections.Generic;
using CountPad.Domain.Common;
using CountPad.Domain.Entities.Identities;

namespace CountPad.Domain.Entities.Users
{
	public class User : BaseAuditableEntity
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Password { get; set; }

		public virtual ICollection<Role> Roles { get; set; }
	}
}
