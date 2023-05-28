using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.ProductCategories.Commands.CreateProductCategory;
using CountPad.Application.UseCases.ProductCategories.Commands.DeleteProductCategory;
using CountPad.Application.UseCases.ProductCategories.Commands.UpdateProductCategory;
using CountPad.Application.UseCases.ProductCategories.Models;
using CountPad.Application.UseCases.ProductCategories.Queries.GetProductCategories;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class ProductCategoryCategoriesController : ApiControllerBase
	{
		[HttpPost]
		public async ValueTask<ActionResult<ProductCategoryDto>> PostProductCategoryAsync(CreateProductCategoryCommand command)
		{
			ProductCategoryDto dto = await Mediator.Send(command);

			return Ok(dto);
		}

		[HttpGet("{ProductCategoryId}")]
		public async ValueTask<ActionResult<ProductCategoryDto>> GetProductCategoryAsync(Guid ProductCategoryId)
		{
			return await Mediator.Send(new GetProductCategoryQuery(ProductCategoryId));
		}

		[HttpGet]
		public async ValueTask<ActionResult<ProductCategoryDto[]>> GetProductCategoriesAsync()
		{
			return await Mediator.Send(new GetProductCategoriesQuery());
		}

		[HttpGet("pagination")]
		public async ValueTask<ActionResult<PaginatedList<ProductCategoryDto>>> GetProductCategoriesWithPaginated([FromQuery] GetProductCategorysWithPaginationQuery query)
		{
			return await Mediator.Send(query);
		}

		[HttpPut]
		public async ValueTask<ActionResult<ProductCategoryDto>> PutProductCategoryAsync(UpdateProductCategoryCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpDelete("{ProductCategoryId}")]
		public async ValueTask<ActionResult<ProductCategoryDto>> DeleteProductCategoryAsync(Guid ProductCategoryId)
		{
			return await Mediator.Send(new DeleteProductCategoryCommand(ProductCategoryId));
		}
	}
}
