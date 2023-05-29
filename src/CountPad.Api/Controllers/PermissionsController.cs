using CountPad.Application.UseCases.Permissions.Commands.CreatePermission;
using CountPad.Application.UseCases.Permissions.Commands.DeletePermission;
using CountPad.Application.UseCases.Permissions.Commands.UpdatePermission;
using CountPad.Application.UseCases.Permissions.Queries;
using CountPad.Application.UseCases.Permissions.Queries.GetPermission;
using CountPad.Application.UseCases.Permissions.Queries.GetPermissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class PermissionsController : ApiControllerBase
	{
		[HttpPost, Authorize(Roles = "createpermission")]
		public async ValueTask<ActionResult<PermissionDto>> PostPermissionAsync(CreatePermissionCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpGet("{permissionId}"), Authorize(Roles = "getpermission")]
		public async ValueTask<ActionResult<PermissionDto>> GetPermissionAsync(Guid permissionId)
		{
			return await Mediator.Send(new GetPermissionQuery(permissionId));
		}

		[HttpGet, Authorize(Roles = "getallpermissions")]
		public async ValueTask<ActionResult<List<PermissionDto>>> GetAllPermissionsAsync()
		{
			return await Mediator.Send(new GetPermissionsQuery());
		}

		[HttpPut, Authorize(Roles = "updatepermission")]
		public async ValueTask<ActionResult<PermissionDto>> PutPermissionAsync(Guid permissionId, UpdatePermissionCommand command)
		{
			if (permissionId != command.Id)
			{
				return BadRequest($"{permissionId} is same with {command.Id}");
			}

			return await Mediator.Send(command);
		}

		[HttpDelete("{permissionId}"), Authorize(Roles = "deletepermission")]
		public async ValueTask<ActionResult<PermissionDto>> DeletePermissionAsync(Guid permissionId)
		{
			return await Mediator.Send(new DeletePermissionCommand(permissionId));
		}
	}
}
