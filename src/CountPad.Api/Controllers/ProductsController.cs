using CountPad.Application.UseCases.Products.Commands.CreateProduct;
using CountPad.Application.UseCases.Products.Models;
using Microsoft.AspNetCore.Mvc;

namespace CountPad.Api.Controllers
{
	public class ProductsController : ApiControllerBase
	{
		[HttpPost]
		public async ValueTask<ActionResult<ProductDto>> PostProductAsync(CreateProductCommand command)
		{
			ProductDto dto = await Mediator.Send(command);

			return Ok(dto);
		}
	}
}
