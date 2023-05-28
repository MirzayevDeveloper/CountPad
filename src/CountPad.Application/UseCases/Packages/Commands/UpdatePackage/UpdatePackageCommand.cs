using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Packages.Models;
using CountPad.Domain.Entities.Products;
using CountPad.Domain.Entities;
using MediatR;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.UseCases.Packages.Commands.CreatePackage;
using CountPad.Domain.Entities.Packages;

namespace CountPad.Application.UseCases.Packages.Commands.UpdatePackage
{
	public class UpdatePackageCommand : IRequest<PackageDto>
	{
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
		public Double Count { get; set; }
		public Decimal IncomingPrice { get; set; }
		public Decimal SalePrice { get; set; }
		public DateTimeOffset IncomingDate { get; set; }
		public Guid DistributorId { get; set; }
	}

	public class UpdatePackageCommandHandler : IRequestHandler<UpdatePackageCommand, PackageDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UpdatePackageCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PackageDto> Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
		{
			Package maybePackage = await
				_context.Packages.FindAsync(new object[] { request.Id });

			ValidatePackIsNotNull(request, maybePackage);

			Distributor maybeDistributor = _context.Distributors
				.SingleOrDefault(d => d.Id.Equals(request.DistributorId));

			Product maybeProduct = _context.Products
				.SingleOrDefault(p => p.Id.Equals(request.ProductId));

			ValidateProductAndDistributorAreNotNull(maybeDistributor, maybeProduct, request);


		}

		private static void ValidatePackIsNotNull(UpdatePackageCommand request, Package maybePackage)
		{
			if (maybePackage == null)
			{
				throw new NotFoundException(nameof(Package), request.Id);
			}
		}

		private void ValidateProductAndDistributorAreNotNull(Distributor maybeDistributor, Product maybeProduct, UpdatePackageCommand request)
		{
			if (maybeDistributor is null)
			{
				throw new NotFoundException(nameof(Distributor), request.DistributorId);
			}
			else if (maybeProduct is null)
			{
				throw new NotFoundException(nameof(Product), request.ProductId);
			}
		}
	}
}
