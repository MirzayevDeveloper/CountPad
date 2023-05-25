using CountPad.Application.UseCases.Permissions.Commands.CreatePermission;
using CountPad.Application.UseCases.Permissions.Commands.DeletePermission;
using CountPad.Application.UseCases.Permissions.Commands.UpdatePermission;
using CountPad.Application.UseCases.Permissions.Models;
using CountPad.Application.UseCases.Permissions.Queries.GetPermission;
using CountPad.Application.UseCases.Permissions.Queries.GetPermissions;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class PermissionController : ApiControllerBase
	{
		[HttpPost]
		public async ValueTask<ActionResult<PermissionDto>> PostPermissionAsync(CreatePermissionCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpGet("{permissionId}")]
		public async ValueTask<ActionResult<PermissionDto>> GetPermissionAsync([FromQuery] GetPermissionQuery query)
		{
			return await Mediator.Send(query);
		}

		[HttpGet]
		public async ValueTask<ActionResult<List<PermissionDto>>> GetAllPermissionsAsync()
		{
			return await Mediator.Send(new GetPermissionsQuery());
		}

		[HttpPut]
		public async ValueTask<ActionResult<PermissionDto>> PutPermissionAsync(Guid permissionId, UpdatePermissionCommand command)
		{
			if (permissionId != command.Id)
			{
				return BadRequest($"{permissionId} is same with {command.Id}");
			}

			return await Mediator.Send(command);
		}

		[HttpDelete("{permissionId}")]
		public async ValueTask<ActionResult<PermissionDto>> DeletePermissionAsync(Guid permissionId)
		{
			return await Mediator.Send(new DeletePermissionCommand(permissionId));
		}
	}
}
