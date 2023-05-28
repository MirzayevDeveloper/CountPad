using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Distributors.Models;
using CountPad.Domain.Entities;
using MediatR;

namespace CountPad.Application.UseCases.Distributors.Commands.UpdateDistributor
{
	public class UpdateDistributorCommand : IRequest<DistributorDto>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string CompanyPhone { get; set; }
		public string DelivererPhone { get; set; }
	}

	public class UpdateDistributorCommandHandler : IRequestHandler<UpdateDistributorCommand, DistributorDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UpdateDistributorCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<DistributorDto> Handle(UpdateDistributorCommand request, CancellationToken cancellationToken)
		{
			Distributor maybeDistributor =
				await _context.Distributors.FindAsync(new object[] { request.Id });

			ValidateDistributorIsNotNull(request, maybeDistributor);

			maybeDistributor.DelivererPhone = request.DelivererPhone;
			maybeDistributor.CompanyPhone = request.CompanyPhone;
			maybeDistributor.Name = request.Name;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<DistributorDto>(maybeDistributor);
		}

		private static void ValidateDistributorIsNotNull(UpdateDistributorCommand request, Distributor maybeDistributor)
		{
			if (maybeDistributor == null)
			{
				throw new NotFoundException(nameof(Distributor), request.Id);
			}
		}
	}
}
