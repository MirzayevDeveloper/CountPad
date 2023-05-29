using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CountPad.Application.UseCases.Permissions.Queries;

namespace CountPad.Application.UseCases.Roles.Models
{
	public class RoleDto
	{
		[JsonPropertyName("role_id")]
		public Guid Id { get; set; }
		public string RoleName { get; set; }

		public ICollection<PermissionDto> Permissions { get; set; }
	}
}
