using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Orders.Models;
using CountPad.Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Orders.Queries.GetOrder
{
	public record GetOrdersQuery : IRequest<OrderDto[]>;

	public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, OrderDto[]>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetOrdersQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<OrderDto[]> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
		{
			Order[] orders = await _context.Orders.ToArrayAsync();

			return _mapper.Map<OrderDto[]>(orders);
		}
	}
}
