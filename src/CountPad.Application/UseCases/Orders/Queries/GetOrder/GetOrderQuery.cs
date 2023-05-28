using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Orders.Models;
using CountPad.Domain.Entities.Orders;
using MediatR;

namespace CountPad.Application.UseCases.Orders.Queries.GetOrder
{
	public record GetOrderQuery(Guid orderId) : IRequest<OrderDto>;

	public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetOrderQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
		{
			Order maybeOrder = await _context.Orders
				.FindAsync(new object[] { request.orderId }, cancellationToken);

			ValidateOrderIsNotNull(request, maybeOrder);

			return _mapper.Map<OrderDto>(maybeOrder);
		}

		private static void ValidateOrderIsNotNull(GetOrderQuery request, Order maybeOrder)
		{
			if (maybeOrder is null)
			{
				throw new NotFoundException(nameof(Order), request.orderId);
			}
		}
	}
}
