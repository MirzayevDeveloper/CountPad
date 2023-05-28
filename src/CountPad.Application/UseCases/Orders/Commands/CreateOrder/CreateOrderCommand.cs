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
using CountPad.Domain.Entities.Products;
using CountPad.Domain.Entities.Users;
using MediatR;

namespace CountPad.Application.UseCases.Orders.Commands.CreateOrder
{
	public class CreateOrderCommand : IRequest<OrderDto>
	{
		public Guid PackageId { get; set; }
		public Guid UserId { get; set; }
		public double Count { get; set; }
		public double SoldPrice { get; set; }
	}

	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CreateOrderCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
			_currentTime = DateTime.Now;
		}

		private DateTimeOffset _currentTime;
		public DateTimeOffset CurrentTime => _currentTime;

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			Package maybePackage = _context.Packages
				.SingleOrDefault(p => p.Id.Equals(request.PackageId));

			User maybeUser = _context.Users
				.SingleOrDefault(u => u.Id.Equals(request.UserId));

			ValidatePackageAndUserAreNotNull(request, maybePackage, maybeUser);

			ValidatePackageItemEnough(request, maybePackage);

			maybePackage.Count -= request.Count;

			var order = new Order
			{
				Package = maybePackage,
				User = maybeUser,
				Count = request.Count,
				SoldDate = CurrentTime,
				SoldPrice = request.SoldPrice,
			};

			_context.Orders.Add(order);

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<OrderDto>(order);
		}

		private static void ValidatePackageItemEnough(CreateOrderCommand request, Package package)
		{
			if (package.Count < request.Count)
			{
				throw new NotFoundException(nameof(Package), $"{request.Count} not enough.");
			}
		}

		private static void ValidatePackageAndUserAreNotNull(CreateOrderCommand request, Package maybePackage, User maybeUser)
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
	}
}
