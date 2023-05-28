using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Distributors.Models;
using CountPad.Domain.Entities;
using MediatR;

namespace CountPad.Application.UseCases.Distributors.Commands.DeleteDistributor
{
	public record DeleteDistributorCommand(Guid distributorId) : IRequest<DistributorDto>;

	public class DeleteDistributorCommandHandler : IRequestHandler<DeleteDistributorCommand, DistributorDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public DeleteDistributorCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<DistributorDto> Handle(DeleteDistributorCommand request, CancellationToken cancellationToken)
		{
			Distributor maybeDistributor = await
				_context.Distributors.FindAsync(new object[] { request.distributorId });

			ValidateDistributorIsNotNull(request, maybeDistributor);

			_context.Distributors.Remove(maybeDistributor);

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<DistributorDto>(maybeDistributor);
		}

		private static void ValidateDistributorIsNotNull(DeleteDistributorCommand request, Distributor maybeDistributor)
		{
			if (maybeDistributor == null)
			{
				throw new NotFoundException(nameof(Distributor), request.distributorId);
			}
		}
	}
}
