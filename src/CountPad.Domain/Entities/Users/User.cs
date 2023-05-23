// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using CountPad.Domain.Common;

namespace CountPad.Domain.Entities.Users
{
	public class User : BaseAuditableEntity
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Password { get; set; }
	}
}
