using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Packages.Models;
using CountPad.Domain.Entities.Packages;
using MediatR;

namespace CountPad.Application.UseCases.Packages.Queries.GetProductsWithPagination
{
	public record GetPackagesWithPaginationQuery : IRequest<PaginatedList<PackageDto>>
	{
		public int PageNumber { get; init; } = 1;
		public int PageSize { get; init; } = 10;
	}

	public class GetPackagesWithPaginationQueryHandler : IRequestHandler<GetPackagesWithPaginationQuery, PaginatedList<PackageDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetPackagesWithPaginationQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PaginatedList<PackageDto>> Handle(GetPackagesWithPaginationQuery request, CancellationToken cancellationToken)
		{
			Package[] products = _context.Packages.ToArray();

			IQueryable<PackageDto> dtos = _mapper.Map<PackageDto[]>(products).AsQueryable();

			PaginatedList<PackageDto> paginatedList =
				await PaginatedList<PackageDto>.CreateAsync(
					dtos, request.PageNumber, request.PageSize);

			return paginatedList;
		}
	}
}
