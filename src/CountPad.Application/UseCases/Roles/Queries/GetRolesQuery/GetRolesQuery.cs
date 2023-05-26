using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Roles.Models;
using CountPad.Domain.Entities.Identities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Roles.Queries.GetRolesQuery
{
	public record GetRolesQuery : IRequest<RoleDto[]>;

	public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, RoleDto[]>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetRolesQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<RoleDto[]> Handle(GetRolesQuery request, CancellationToken cancellationToken)
		{
			Role[] roles = await _context.Roles.ToArrayAsync();

			return _mapper.Map<RoleDto[]>(roles);
		}
	}
}
