using CountPad.Application.UseCases.Distributors.Commands.CreateDistributor;
using CountPad.Application.UseCases.Distributors.Commands.DeleteDistributor;
using CountPad.Application.UseCases.Distributors.Commands.UpdateDistributor;
using CountPad.Application.UseCases.Distributors.Models;
using CountPad.Application.UseCases.Distributors.Queries.GetDistributor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class DistributorsController : ApiControllerBase
	{
		[HttpPost, Authorize(Roles = "createdistributor")]
		public async ValueTask<ActionResult<DistributorDto>> PostDistributorAsync(CreateDistributorCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpGet("{distributorId}"), Authorize(Roles = "getdistributor")]
		public async ValueTask<ActionResult<DistributorDto>> GetDistributorAsync(Guid distributorId)
		{
			return await Mediator.Send(new GetDistributorQuery(distributorId));
		}

		[HttpGet, Authorize(Roles = "getalldistributors")]
		public async ValueTask<ActionResult<DistributorDto[]>> GetAllDistributorsAsync()
		{
			return await Mediator.Send(new GetDistributorsQuery());
		}

		[HttpPut, Authorize(Roles = "updatedistributor")]
		public async ValueTask<ActionResult<DistributorDto>> UpdateDistributorAsync(Guid distributorId, UpdateDistributorCommand command)
		{
			if (distributorId != command.Id)
			{
				return BadRequest();
			}

			return await Mediator.Send(command);
		}

		[HttpDelete("{distributorId}"), Authorize(Roles = "deletedistributor")]
		public async ValueTask<ActionResult<DistributorDto>> DeleteDistributorAsync(Guid distributorId)
		{
			return await Mediator.Send(new DeleteDistributorCommand(distributorId));
		}
	}
}
