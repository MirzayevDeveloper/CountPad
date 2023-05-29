using System;
using System.Text.Json.Serialization;

namespace CountPad.Application.UseCases.Orders.Models
{
	public class RoleOrder
	{
		[JsonPropertyName("role_id")]
		public Guid Id { get; set; }
		public string RoleName { get; set; }
	}
}
