using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Products.Models;
using CountPad.Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Products.Queries.GetProductsWithFilters
{
	public class GetProductsWithFilterQuery : IRequest<PaginatedList<ProductDto>>
	{
		public string Category { get; set; }
		public int PageNumber { get; init; } = 1;
		public int PageSize { get; init; } = 10;
	}

	public class GetProductsWithFilterQueryHandler : IRequestHandler<GetProductsWithFilterQuery, PaginatedList<ProductDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetProductsWithFilterQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithFilterQuery request, CancellationToken cancellationToken)
		{
			Product[] products = await _context.Products
				.Where(p => p.ProductCategory.Name.ToLower()
				.Contains(request.Category.ToLower()))
				.ToArrayAsync(cancellationToken);

			//.Where(p => EF.Functions.ILike(p.Name.ToLower(), $"%{request.ProductName.ToLower()}%"))

			List<ProductDto> dtos = _mapper.Map<ProductDto[]>(products).ToList();

			PaginatedList<ProductDto> paginatedList =
				 PaginatedList<ProductDto>.CreateAsync(
					dtos, request.PageNumber, request.PageSize);

			return paginatedList;
		}
	}
}
