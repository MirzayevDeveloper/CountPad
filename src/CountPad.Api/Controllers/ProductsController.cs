using CountPad.Api.Filters;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Products.Commands.CreateProduct;
using CountPad.Application.UseCases.Products.Commands.DeleteProduct;
using CountPad.Application.UseCases.Products.Commands.UpdateProduct;
using CountPad.Application.UseCases.Products.Models;
using CountPad.Application.UseCases.Products.Queries.GetProducts;
using CountPad.Application.UseCases.Products.Queries.GetProductsWithFilters;
using CountPad.Application.UseCases.Products.Queries.GetProductsWithPagination;
using LazyCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CountPad.Api.Controllers
{
	public class ProductsController : ApiControllerBase
	{
		private readonly IAppCache _cache;
		private const string My_First_Key = "First";
		private const string My_Second_Key = "Second";

		public ProductsController(IAppCache cache)
		{
			_cache = cache;
		}

		[HttpPost, Authorize(Roles = "createproduct"), LogEndpoint, AllowAnonymous]
		public async ValueTask<ActionResult<ProductDto>> PostProductAsync(CreateProductCommand command)
		{
			ProductDto dto = await Mediator.Send(command);

			_cache.Remove(My_First_Key);

			return Ok(dto);
		}

		[HttpGet("{productId}"), Authorize(Roles = "getproduct")]
		public async ValueTask<ActionResult<ProductDto>> GetProductAsync(Guid productId)
		{
			return await Mediator.Send(new GetProductQuery(productId));
		}

		/*[HttpGet, BasicAuthentication(Roles = "getallproducts")]
		public async ValueTask<ActionResult<ProductDto[]>> GetProductsAsync()
		{
			return await Mediator.Send(new GetProductsQuery());
		}*/

		[HttpGet, Authorize(Roles = "getproductswithpagination"), AllowAnonymous]
		public async ValueTask<ActionResult<PaginatedList<ProductDto>>> GetProductsWithPaginated([FromQuery] GetProductsWithPaginationQuery query)
		{
			return await _cache.GetOrAddAsync(My_First_Key, x =>
			{
				x.SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
				return Mediator.Send(query);
			});
		}

		[HttpGet("Filter"), Authorize(Roles = "getproductswithfilter"), AllowAnonymous]
		public async ValueTask<ActionResult<PaginatedList<ProductDto>>> GetProductsWithFilter([FromQuery] GetProductsWithFilterQuery query)
		{
			return await _cache.GetOrAddAsync(My_Second_Key, x =>
			{
				x.SetAbsoluteExpiration(TimeSpan.FromMinutes(20));
				return Mediator.Send(query);
			});
		}

		[HttpPut, Authorize(Roles = "updateproduct")]
		public async ValueTask<ActionResult<ProductDto>> PutProductAsync(UpdateProductCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpDelete("{productId}"), Authorize(Roles = "deleteproduct")]
		public async ValueTask<ActionResult<ProductDto>> DeleteProductAsync(Guid productId)
		{
			return await Mediator.Send(new DeleteProductCommand(productId));
		}
	}
}
