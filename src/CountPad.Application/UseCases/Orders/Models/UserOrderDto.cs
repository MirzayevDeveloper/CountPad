using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CountPad.Application.UseCases.Orders.Models
{
	public class UserOrderDto
	{
		[JsonPropertyName("user_id")]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Password { get; set; }
		public ICollection<RoleOrder> Roles { get; set; }
	}
}
