// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using CountPad.Domain.Common.BaseEntities;
using CountPad.Domain.Entities.Identities;

namespace CountPad.Domain.Entities.Users
{
	public class User : BaseAuditableEntity
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Password { get; set; }

		public virtual ICollection<Role> Roles { get; set; }

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			foreach (Role role in Roles)
			{
				stringBuilder.Append($"Role Id: {role.Id}\nRole name: {role.RoleName}");
			}

			return $"Id: {Id}\nName: {Name}\nPhone: {Phone}\nRoles{stringBuilder}";
		}
	}
}
