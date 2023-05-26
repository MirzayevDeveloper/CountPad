﻿using CountPad.Application.UseCases.Roles.Commands.CreateRole;
using CountPad.Application.UseCases.Roles.Commands.UpdateRole;
using CountPad.Application.UseCases.Roles.Models;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class RolesController : ApiControllerBase
	{
		[HttpPost]
		public async ValueTask<ActionResult<RoleDto>> PostRoleAsync(CreateRoleCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpGet("{roleId}")]
		public async ValueTask<ActionResult<RoleDto>> GetRoleAsync(Guid roleId)
		{
			return await Mediator.Send(new GetRoleQuery(roleId));
		}

		[HttpGet]
		public async ValueTask<ActionResult<List<RoleDto>>> GetAllRoles()
		{
			return await Mediator.Send(new GetRolesQuery());
		}

		[HttpPut]
		public async ValueTask<ActionResult<RoleDto>> PutRoleAsync(Guid roleId, UpdateRoleCommand command)
		{
			if (roleId != command.Id)
			{
				return BadRequest($"{roleId} is same with {command.Id}");
			}

			return await Mediator.Send(command);
		}
	}
}
