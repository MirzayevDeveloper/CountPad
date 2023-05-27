using System;
using System.Collections.Generic;
using CountPad.Application.UseCases.Permissions.Models;

namespace CountPad.Application.UseCases.Roles.Models
{
	public class RoleDto
	{
		public Guid Id { get; set; }
		public string RoleName { get; set; }

		public ICollection<PermissionDto> Permissions { get; set; }
	}
}
