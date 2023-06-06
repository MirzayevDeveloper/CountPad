using CountPad.Api.Filters;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Packages.Commands.CreatePackage;
using CountPad.Application.UseCases.Packages.Commands.DeletePackage;
using CountPad.Application.UseCases.Packages.Commands.UpdatePackage;
using CountPad.Application.UseCases.Packages.Models;
using CountPad.Application.UseCases.Packages.Queries.GetPackage;
using CountPad.Application.UseCases.Packages.Queries.GetProductsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class PackagesController : ApiControllerBase
	{
		[HttpPost, Authorize(Roles = "createpackage")]
		public async ValueTask<ActionResult<PackageDto>> PostPackageAsync(CreatePackageCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpGet("{packageId}"), Authorize(Roles = "getpackage")]
		public async ValueTask<ActionResult<PackageDto>> GetPackageAsync(Guid packageId)
		{
			return await Mediator.Send(new GetPackageQuery(packageId));
		}

		[HttpGet, Authorize(Roles = "getallpackages"), RedisCache(10, 50)]
		public async ValueTask<ActionResult<PackageDto[]>> GetPackagesAsync()
		{
			return await Mediator.Send(new GetPackagesQuery());
		}

		[HttpGet("pagination"), Authorize(Roles = "getpackageswithpagination"), RedisCache(10, 50)]
		public async ValueTask<ActionResult<PaginatedList<PackageDto>>> GetPackagesWithPaginated([FromQuery] GetPackagesWithPaginationQuery query)
		{
			return await Mediator.Send(query);
		}

		[HttpPut, Authorize(Roles = "updatepackage")]
		public async ValueTask<ActionResult<PackageDto>> PutPackageAsync(UpdatePackageCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpDelete("{packageId}"), Authorize(Roles = "deletepackage")]
		public async ValueTask<ActionResult<PackageDto>> DeletePackageAsync(Guid packageId)
		{
			return await Mediator.Send(new DeletePackageCommand(packageId));
		}
	}
}
