using System;
using System.Text.Json.Serialization;

namespace CountPad.Application.UseCases.Permissions.Queries
{
	public class PermissionDto
	{
		[JsonPropertyName("permission_id")]
		public Guid Id { get; set; }
		public string PermissionName { get; set; }
	}
}
