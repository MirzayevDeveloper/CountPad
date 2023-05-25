// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;
using CountPad.Domain.Common;
using CountPad.Domain.Entities.Users;

namespace CountPad.Domain.Entities.Identities
{
	public class UserRole : BaseEntity
	{
        public Guid UserId { get; set; }
		public User User { get; set; }

		public Guid RoleId { get; set; }
		public Role Role { get; set; }
	}
}
