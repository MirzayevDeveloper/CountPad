using System;
using MediatR;

namespace CountPad.Application.UseCases.Roles.Commands
{
	public record DeleteRoleCommand(Guid roleId) : IRequest;


}
