using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Packages.Models;
using CountPad.Domain.Entities.Packages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Packages.Queries.GetPackage
{
	public record GetPackagesQuery : IRequest<PackageDto[]>;

	public class GetPackagesQueryHandler : IRequestHandler<GetPackagesQuery, PackageDto[]>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetPackagesQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PackageDto[]> Handle(GetPackagesQuery request, CancellationToken cancellationToken)
		{
			Package[] packages = await _context.Packages.ToArrayAsync();

			return _mapper.Map<PackageDto[]>(packages);
		}
	}
}
