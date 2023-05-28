using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Distributors.Models;
using CountPad.Domain.Entities;
using MediatR;

namespace CountPad.Application.UseCases.Distributors.Queries.GetDistributor
{
	public record GetDistributorQuery(Guid distributorId) : IRequest<DistributorDto>;

	public class GetDistributorQueryHandler : IRequestHandler<GetDistributorQuery, DistributorDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetDistributorQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<DistributorDto> Handle(GetDistributorQuery request, CancellationToken cancellationToken)
		{
			Distributor maybeDistributor = await
				_context.Distributors.FindAsync(new object[] { request.distributorId });

			ValidateDistributorIsNotNull(request, maybeDistributor);

			return _mapper.Map<DistributorDto>(maybeDistributor);
		}

		private static void ValidateDistributorIsNotNull(GetDistributorQuery request, Distributor maybeDistributor)
		{
			if (maybeDistributor == null)
			{
				throw new NotFoundException(nameof(Distributor), request.distributorId);
			}
		}
	}
}
