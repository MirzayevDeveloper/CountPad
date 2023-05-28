using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Packages.Models;
using CountPad.Domain.Entities;
using CountPad.Domain.Entities.Packages;
using CountPad.Domain.Entities.Products;
using MediatR;

namespace CountPad.Application.UseCases.Packages.Commands.CreatePackage
{
	public record CreatePackageCommand : IRequest<PackageDto>
	{
		public Guid ProductId { get; set; }
		public Double Count { get; set; }
		public Decimal IncomingPrice { get; set; }
		public Decimal SalePrice { get; set; }
		public DateTimeOffset IncomingDate { get; set; }
		public Guid DistributorId { get; set; }
	}

	public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, PackageDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CreatePackageCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PackageDto> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
		{
			Distributor maybeDistributor = _context.Distributors
				.SingleOrDefault(d => d.Id.Equals(request.DistributorId));

			Product maybeProduct = _context.Products
				.SingleOrDefault(p => p.Id.Equals(request.ProductId));

			ValidateProductAndDistributorAreNotNull(maybeDistributor, maybeProduct, request);

			var package = new Package
			{
				Product = maybeProduct,
				Count = request.Count,
				IncomingPrice = request.IncomingPrice,
				SalePrice = request.SalePrice,
				IncomingDate = request.IncomingDate,
				Distributor = maybeDistributor,
			};

			package = _context.Packages.Add(package).Entity;
			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<PackageDto>(package);
		}

		private void ValidateProductAndDistributorAreNotNull(Distributor maybeDistributor, Product maybeProduct, CreatePackageCommand request)
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
