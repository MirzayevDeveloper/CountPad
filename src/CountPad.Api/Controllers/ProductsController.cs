using CountPad.Api.Filters;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Products.Commands.CreateProduct;
using CountPad.Application.UseCases.Products.Commands.DeleteProduct;
using CountPad.Application.UseCases.Products.Commands.UpdateProduct;
using CountPad.Application.UseCases.Products.Models;
using CountPad.Application.UseCases.Products.Queries.GetProducts;
using CountPad.Application.UseCases.Products.Queries.GetProductsWithFilters;
using CountPad.Application.UseCases.Products.Queries.GetProductsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        [HttpPost, Authorize(Roles = "createproduct"), LogEndpoint, AllowAnonymous]
        public async ValueTask<ActionResult<ProductDto>> PostProductAsync(CreateProductCommand command)
        {
            ProductDto dto = await Mediator.Send(command);

            return Ok(dto);
        }

        [HttpGet("{productId}"), Authorize(Roles = "getproduct")]
        public async ValueTask<ActionResult<ProductDto>> GetProductAsync(Guid productId)
        {
            return await Mediator.Send(new GetProductQuery(productId));
        }

        [HttpGet, Authorize(Roles = "getproductswithpagination"), AllowAnonymous, RedisCache(10, 30)]
        public async ValueTask<ActionResult<PaginatedList<ProductDto>>> GetProductsWithPaginated([FromQuery] GetProductsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("Filter"), Authorize(Roles = "getproductswithfilter"), AllowAnonymous, Cache(10, 30)]
        public async ValueTask<ActionResult<PaginatedList<ProductDto>>> GetProductsWithFilter([FromQuery] GetProductsWithFilterQuery query)
        {
            return await Mediator.Send(query);
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
