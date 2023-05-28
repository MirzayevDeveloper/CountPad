using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Distributors.Models;
using CountPad.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Distributors.Queries.GetDistributor
{
	public record GetDistributorsQuery : IRequest<DistributorDto[]>;

	public class GetDistributorsQueryHandler : IRequestHandler<GetDistributorsQuery, DistributorDto[]>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetDistributorsQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<DistributorDto[]> Handle(GetDistributorsQuery request, CancellationToken cancellationToken)
		{
			Distributor[] distributors = await _context.Distributors.ToArrayAsync();

			return _mapper.Map<DistributorDto[]>(distributors);
		}
	}
}
