using System.Collections.Generic;
using CountPad.Application.UseCases.Roles.Models;
using MediatR;

namespace CountPad.Application.UseCases.Roles.Queries
{
	public record GetRolesQuery : IRequest<List<RoleDto>>;
}
