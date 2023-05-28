using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Orders.Models;
using CountPad.Domain.Entities.Orders;
using CountPad.Domain.Entities.Packages;
using MediatR;

namespace CountPad.Application.UseCases.Orders.Commands.DeleteOrder
{
	public record DeleteOrderCommand(Guid Id, bool IsReverse) : IRequest<OrderDto>;

	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, OrderDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public DeleteOrderCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<OrderDto> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
		{
			Order maybeOrder = await _context.Orders
				.FindAsync(new object[] { request.Id }, cancellationToken);

			ValidateOrderIsNotNull(request, maybeOrder);
			ValidateReverseAndConfigure(request, maybeOrder);

			_context.Orders.Remove(maybeOrder);

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<OrderDto>(maybeOrder);
		}

		private void ValidateReverseAndConfigure(DeleteOrderCommand request, Order maybeOrder)
		{
			if (request.IsReverse)
			{
				Package maybePackage = _context.Packages
					.SingleOrDefault(p => p.Id.Equals(maybeOrder.PackageId));

				maybePackage.Count += maybeOrder.Count;
			}
		}

		private static void ValidateOrderIsNotNull(DeleteOrderCommand request, Order maybeOrder)
		{
			if (maybeOrder is null)
			{
				throw new NotFoundException(nameof(Order), request.Id);
			}
		}
	}
}
