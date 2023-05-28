using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Orders.Models;
using CountPad.Domain.Entities.Orders;
using CountPad.Domain.Entities.Packages;
using MediatR;

namespace CountPad.Application.UseCases.Orders.Commands.UpdateOrder
{
	public class UpdateOrderCommand : IRequest<OrderDto>
	{
		public Guid Id { get; set; }
		public Guid PackageId { get; set; }
		public Guid UserId { get; set; }
		public double Count { get; set; }
	}

	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UpdateOrderCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<OrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
		{
			Order maybeOrder = await
				_context.Orders.FindAsync(new object[] { request.Id });

			ValidateOrderIsNotNull(request, maybeOrder);

			return null;
		}

		private static void ValidateOrderIsNotNull(UpdateOrderCommand request, Order maybeOrder)
		{
			if (maybeOrder == null)
			{
				throw new NotFoundException(nameof(Order), request.Id);
			}
		}
	}
}
