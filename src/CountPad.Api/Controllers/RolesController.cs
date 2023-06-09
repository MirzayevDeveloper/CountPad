﻿using CountPad.Application.UseCases.Roles.Commands.CreateRole;
using CountPad.Application.UseCases.Roles.Commands.DeleteRole;
using CountPad.Application.UseCases.Roles.Commands.UpdateRole;
using CountPad.Application.UseCases.Roles.Models;
using CountPad.Application.UseCases.Roles.Queries.GetRoleQuery;
using CountPad.Application.UseCases.Roles.Queries.GetRolesQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class RolesController : ApiControllerBase
	{
		[HttpPost, Authorize(Roles = "createrole")]
		public async ValueTask<ActionResult<RoleDto>> PostRoleAsync(CreateRoleCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpGet("{roleId}"), Authorize(Roles = "getrole")]
		public async ValueTask<ActionResult<RoleDto>> GetRoleAsync(Guid roleId)
		{
			return await Mediator.Send(new GetRoleQuery(roleId));
		}

		[HttpGet, Authorize(Roles = "getallroles")]
		public async ValueTask<ActionResult<RoleDto[]>> GetAllRoles()
		{
			return await Mediator.Send(new GetRolesQuery());
		}

		[HttpPut, Authorize(Roles = "updaterole")]
		public async ValueTask<ActionResult<RoleDto>> PutRoleAsync(Guid roleId, UpdateRoleCommand command)
		{
			if (roleId != command.Id)
			{
				return BadRequest($"{roleId} is same with {command.Id}");
			}

			return await Mediator.Send(command);
		}

		[HttpDelete("{roleId}"), Authorize(Roles = "deleterole")]
		public async ValueTask<ActionResult<RoleDto>> DeleteRoleAsync(Guid roleId)
		{
			return await Mediator.Send(new DeleteRoleCommand(roleId));
		}
	}
}
