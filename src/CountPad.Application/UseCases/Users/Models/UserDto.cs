using System.Collections.Generic;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Roles.Models;

namespace CountPad.Application.UseCases.Users.Models
{
	public class UserDto : BaseAuditableEntityDto
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Password { get; set; }

		public ICollection<RoleDto> Roles { get; set; }
	}
}
