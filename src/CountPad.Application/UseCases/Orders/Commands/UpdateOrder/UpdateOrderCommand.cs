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
using CountPad.Domain.Entities.Users;
using MediatR;

namespace CountPad.Application.UseCases.Orders.Commands.UpdateOrder
{
	public class UpdateOrderCommand : IRequest<OrderDto>
	{
		public Guid Id { get; set; }
		public Guid PackageId { get; set; }
		public Guid UserId { get; set; }
		public double SoldPrice { get; set; }
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

			Package maybePackage = _context.Packages
				.SingleOrDefault(p => p.Id.Equals(request.PackageId));

			User maybeUser = _context.Users
				.SingleOrDefault(u => u.Id.Equals(request.UserId));

			ValidatePackageAndUserAreNotNull(request, maybePackage, maybeUser);

			maybePackage.Count += maybeOrder.Count;
			maybeOrder.Count = request.Count;
			maybePackage.Count -= maybeOrder.Count;
			maybeOrder.SoldPrice = request.SoldPrice;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<OrderDto>(maybeOrder);
		}

		private static void ValidatePackageAndUserAreNotNull(UpdateOrderCommand request, Package maybePackage, User maybeUser)
		{
			if (maybePackage is null)
			{
				throw new NotFoundException(nameof(Package), request.PackageId);
			}
			else if (maybeUser is null)
			{
				throw new NotFoundException(nameof(User), request.UserId);
			}
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
