using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Orders.Commands.CreateOrder;
using CountPad.Application.UseCases.Orders.Commands.DeleteOrder;
using CountPad.Application.UseCases.Orders.Commands.UpdateOrder;
using CountPad.Application.UseCases.Orders.Models;
using CountPad.Application.UseCases.Orders.Queries.GetOrder;
using CountPad.Application.UseCases.Orders.Queries.GetOrdersWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace CountPad.Api.Controllers
{
	public class OrdersController : ApiControllerBase
	{
		[HttpPost, Authorize(Roles = "createorder")]
		public async ValueTask<ActionResult<OrderDto>> PostOrderAsync(CreateOrderCommand command)
		{
			return await Mediator.Send(command);
		}

		[HttpGet("{orderId}"), Authorize(Roles = "getorder")]
		public async ValueTask<ActionResult<OrderDto>> GetOrderAsync(Guid orderId)
		{
			return await Mediator.Send(new GetOrderQuery(orderId));
		}

		[HttpGet, Authorize(Roles = "getallorders"), ResponseCache(Duration = 30)]
		public async ValueTask<ActionResult<OrderDto[]>> GetOrdersAsync()
		{
			return await Mediator.Send(new GetOrdersQuery());
		}

		[HttpGet("pagination"), Authorize(Roles = "getorderswithpagination"), OutputCache(Duration = 30)]
		public async ValueTask<ActionResult<PaginatedList<OrderDto>>> GetOrdersWithPaginated([FromQuery] GetOrdersWithPaginationQuery query)
		{
			return await Mediator.Send(query);
		}

		[HttpPut, Authorize(Roles = "updateorder")]
		public async ValueTask<ActionResult<OrderDto>> PutOrderAsync(Guid orderId, UpdateOrderCommand command)
		{
			if (orderId != command.Id)
			{
				return BadRequest();
			}

			return await Mediator.Send(command);
		}

		[HttpDelete, Authorize(Roles = "deleteOrder")]
		public async ValueTask<ActionResult<OrderDto>> DeleteOrderAsync([FromQuery] DeleteOrderCommand command)
		{
			return await Mediator.Send(command);
		}
	}
}
