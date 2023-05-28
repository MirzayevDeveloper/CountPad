using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Packages.Commands.UpdatePackage;
using CountPad.Application.UseCases.Packages.Models;
using CountPad.Domain.Entities.Packages;
using MediatR;

namespace CountPad.Application.UseCases.Packages.Queries.GetPackage
{
	public record GetPackageQuery(Guid packageId) : IRequest<PackageDto>;

	public class GetPackageQueryHandler : IRequestHandler<GetPackageQuery, PackageDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetPackageQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PackageDto> Handle(GetPackageQuery request, CancellationToken cancellationToken)
		{
			Package maybePackage = await
				_context.Packages.FindAsync(new object[] { request.packageId });

			ValidatePackIsNotNull(request, maybePackage);

			return _mapper.Map<PackageDto>(maybePackage);
		}

		private static void ValidatePackIsNotNull(GetPackageQuery request, Package maybePackage)
		{
			if (maybePackage == null)
			{
				throw new NotFoundException(nameof(Package), request.packageId);
			}
		}
	}
}
