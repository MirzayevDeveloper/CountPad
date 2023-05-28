using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Distributors.Models;
using CountPad.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CountPad.Application.UseCases.Distributors.Commands.CreateDistributor
{
	public class CreateDistributorCommand : IRequest<DistributorDto>
	{
		private string _name;
		public string Name
		{
			get { return _name; }
			set { _name = value.ToLower(); }
		}

		public string CompanyPhone { get; set; }
		public string DelivererPhone { get; set; }
	}

	public class CreateDistributorCommandHandler : IRequestHandler<CreateDistributorCommand, DistributorDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CreateDistributorCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<DistributorDto> Handle(CreateDistributorCommand request, CancellationToken cancellationToken)
		{
			Distributor maybeDistributor =
				_context.Distributors.SingleOrDefault(d => d.Name.Equals(request.Name));

			ValidateDistributorIsNull(request, maybeDistributor);

			maybeDistributor = _context.Distributors.Add(new()
			{
				Name = request.Name,
				DelivererPhone = request.DelivererPhone,
				CompanyPhone = request.CompanyPhone,
			}).Entity;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<DistributorDto>(maybeDistributor);
		}

		private static void ValidateDistributorIsNull(CreateDistributorCommand request, Distributor maybeDistributor)
		{
			if (maybeDistributor != null)
			{
				throw new AlreadyExistsException(nameof(Distributor), request.Name);
			}
		}
	}
}
