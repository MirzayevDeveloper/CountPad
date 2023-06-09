﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Orders.Models;
using CountPad.Domain.Entities.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Orders.Queries.GetOrdersWithPagination
{
	public record GetOrdersWithPaginationQuery : IRequest<PaginatedList<OrderDto>>
	{
		public int PageNumber { get; init; } = 1;
		public int PageSize { get; init; } = 10;
	}

	public class GetOrdersWithPaginationQueryHandler : IRequestHandler<GetOrdersWithPaginationQuery, PaginatedList<OrderDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetOrdersWithPaginationQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PaginatedList<OrderDto>> Handle(GetOrdersWithPaginationQuery request, CancellationToken cancellationToken)
		{
			Order[] orders = await _context.Orders.ToArrayAsync();

			List<OrderDto> dtos = _mapper.Map<OrderDto[]>(orders).ToList();

			PaginatedList<OrderDto> paginatedList =
				 PaginatedList<OrderDto>.CreateAsync(
					dtos, request.PageNumber, request.PageSize);

			return paginatedList;
		}
	}
}
