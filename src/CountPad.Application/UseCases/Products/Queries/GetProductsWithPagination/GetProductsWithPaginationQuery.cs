﻿using System.Collections.Generic;
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

namespace CountPad.Application.UseCases.Products.Queries.GetProductsWithPagination
{
	public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
	{
		public int PageNumber { get; init; } = 1;
		public int PageSize { get; init; } = 10;
	}

	public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetProductsWithPaginationQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
		{
			Product[] products = await _context.Products.ToArrayAsync();

			List<ProductDto> dtos = _mapper.Map<ProductDto[]>(products).ToList();

			PaginatedList<ProductDto> paginatedList =
				 PaginatedList<ProductDto>.CreateAsync(
					dtos, request.PageNumber, request.PageSize);

			return paginatedList;
		}
	}
}
