using CountPad.Application.UseCases.Users.Commands.CreateUser;
using CountPad.Application.UseCases.Users.Commands.DeleteUser;
using CountPad.Application.UseCases.Users.Commands.UpdateUser;
using CountPad.Application.UseCases.Users.Models;
using CountPad.Application.UseCases.Users.Queries.GetUser;
using CountPad.Application.UseCases.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class UsersController : ApiControllerBase
	{

		[HttpPost, AllowAnonymous]
		public async ValueTask<ActionResult<UserDto>> PostUserAsync([FromBody] CreateUserCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpGet("{userId}"), Authorize(Roles = "getuser")]
		public async ValueTask<ActionResult<UserDto>> GetUserAsync(Guid userId)
		{
			return await Mediator.Send(new GetUserQuery(userId));
		}

		[HttpGet] //, Authorize(Roles = "getallusers")
		public async ValueTask<ActionResult<UserDto[]>> GetAllUsersAsync()
		{
			return await Mediator.Send(new GetUsersQuery());
		}

		[HttpPut, Authorize(Roles = "updateuser"), AllowAnonymous]
		public async ValueTask<ActionResult<UserDto>> PutUserAsync([FromQuery] Guid userId, [FromBody] UpdateUserCommand command)
		{
			if (userId != command.Id)
			{
				return BadRequest();
			}

			return await Mediator.Send(command);
		}

		[HttpDelete("{userId}"), Authorize(Roles = "deleteuser")]
		public async ValueTask<ActionResult<UserDto>> DeleteUserAsync(Guid userId)
		{
			return await Mediator.Send(new DeleteUserCommand(userId));
		}
	}
}
