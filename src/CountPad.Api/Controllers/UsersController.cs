using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Users.Commands.CreateUser;
using CountPad.Application.UseCases.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class UsersController : ApiControllerBase
	{
		[HttpPost]
		public async ValueTask<ActionResult<ResponseCore<UserDto>>> PostUserAsync(CreateUserCommand command)
		{
			return await Mediator.Send(command);
		}
	}
}
