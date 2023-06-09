﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CountPad.Application.UseCases.Roles.Models;

namespace CountPad.Application.UseCases.Users.Models
{
	public class UserDto
	{
		[JsonPropertyName("user_id")]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Password { get; set; }

		public ICollection<RoleDto> Roles { get; set; }
	}
}
