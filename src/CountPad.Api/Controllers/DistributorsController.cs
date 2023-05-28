using CountPad.Application.UseCases.Distributors.Commands.CreateDistributor;
using CountPad.Application.UseCases.Distributors.Commands.DeleteDistributor;
using CountPad.Application.UseCases.Distributors.Commands.UpdateDistributor;
using CountPad.Application.UseCases.Distributors.Models;
using CountPad.Application.UseCases.Distributors.Queries.GetDistributor;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class DistributorsController : ApiControllerBase
	{
		[HttpPost]
		public async ValueTask<ActionResult<DistributorDto>> PostDistributorAsync(CreateDistributorCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpGet("{distributorId}")]
		public async ValueTask<ActionResult<DistributorDto>> GetDistributorAsync(Guid distributorId)
		{
			return await Mediator.Send(new GetDistributorQuery(distributorId));
		}

		[HttpGet]
		public async ValueTask<ActionResult<DistributorDto[]>> GetAllDistributorsAsync()
		{
			return await Mediator.Send(new GetDistributorsQuery());
		}

		[HttpPut]
		public async ValueTask<ActionResult<DistributorDto>> UpdateDistributorAsync(UpdateDistributorCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpDelete("{distributorId}")]
		public async ValueTask<ActionResult<DistributorDto>> DeleteDistributorAsync(Guid distributorId)
		{
			return await Mediator.Send(new DeleteDistributorCommand(distributorId));
		}
	}
}
