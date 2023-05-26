using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountPad.Application.UseCases.Roles.Models;
using MediatR;

namespace CountPad.Application.UseCases.Roles.Queries
{
	public record GetRolesQuery : IRequest<List<RoleDto>>;
}
