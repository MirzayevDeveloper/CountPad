using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountPad.Application.Common.Models;
using CountPad.Domain.Entities.Identities;

namespace CountPad.Application.UseCases.Users.Models
{
	public class UserDto : BaseAuditableEntityDto
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Password { get; set; }

		public ICollection<Role> Roles { get; set; }
	}
}
