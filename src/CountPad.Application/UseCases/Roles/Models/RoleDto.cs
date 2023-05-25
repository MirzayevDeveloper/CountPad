using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountPad.Application.UseCases.Permissions.Models;

namespace CountPad.Application.UseCases.Roles.Models
{
	public class RoleDto
	{
		public string RoleName { get; set; }

        public PermissionDto[] Permissions { get; set; }
    }
}
