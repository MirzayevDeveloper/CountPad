using CountPad.Api.Filters;
using CountPad.Application.UseCases.Authorizations.Logins;
using CountPad.Application.UseCases.Authorizations.RefreshToken;
using CountPad.Domain.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class AuthenticationController : ApiControllerBase
	{
		[HttpPost, AllowAnonymous, LogEndpoint, AddCustomHeader(headerName: "Login", headerValue: "Password")]
		public async ValueTask<ActionResult<UserToken>> LoginAsync([FromQuery] LoginCommand command)
		{
			UserToken maybeToken = await Mediator.Send(command);

			return Ok(maybeToken);
		}

		[HttpPut, AllowAnonymous]
		public async ValueTask<ActionResult<UserToken>> UpdateToken([FromQuery] Guid refreshId, [FromBody] RefreshTokenCommand command)
		{
			if (refreshId != command.Id)
			{
				return BadRequest();
			}

			UserToken maybeToken = await Mediator.Send(command);

			if (maybeToken == null)
			{
				return Unauthorized("Invalid attempt!");
			}

			return maybeToken.Id == default ? Ok("Authorized") : Ok(maybeToken);
		}
	}
}
