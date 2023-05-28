using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.ProductCategories.Commands.CreateProductCategory;
using CountPad.Application.UseCases.ProductCategories.Commands.DeleteProductCategory;
using CountPad.Application.UseCases.ProductCategories.Commands.UpdateProductCategory;
using CountPad.Application.UseCases.ProductCategories.Models;
using CountPad.Application.UseCases.ProductCategories.Queries.GetProductCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class ProductCategoriesController : ApiControllerBase
	{
		[HttpPost, Authorize(Roles = "createproductcategory")]
		public async ValueTask<ActionResult<ProductCategoryDto>> PostProductCategoryAsync(CreateProductCategoryCommand command)
		{
			ProductCategoryDto dto = await Mediator.Send(command);

			return Ok(dto);
		}

		[HttpGet("{ProductCategoryId}"), Authorize(Roles = "getproductcategory")]
		public async ValueTask<ActionResult<ProductCategoryDto>> GetProductCategoryAsync(Guid ProductCategoryId)
		{
			return await Mediator.Send(new GetProductCategoryQuery(ProductCategoryId));
		}

		[HttpGet, Authorize(Roles = "getallproductcategories")]
		public async ValueTask<ActionResult<ProductCategoryDto[]>> GetProductCategoriesAsync()
		{
			return await Mediator.Send(new GetProductCategoriesQuery());
		}

		[HttpPut, Authorize(Roles = "updateproductcategory")]
		public async ValueTask<ActionResult<ProductCategoryDto>> PutProductCategoryAsync(UpdateProductCategoryCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpDelete("{ProductCategoryId}"), Authorize(Roles = "deleteproductcategory")]
		public async ValueTask<ActionResult<ProductCategoryDto>> DeleteProductCategoryAsync(Guid ProductCategoryId)
		{
			return await Mediator.Send(new DeleteProductCategoryCommand(ProductCategoryId));
		}
	}
}
