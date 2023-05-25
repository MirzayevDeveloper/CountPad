using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Permissions.Models;
using CountPad.Domain.Entities.Identities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Permissions.Queries.GetPermissions
{
	public record GetPermissionsQuery : IRequest<List<PermissionDto>>;

	public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<PermissionDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetPermissionsQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<List<PermissionDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
		{
			List<Permission> entities = await _context.Permissions.ToListAsync();

			return _mapper.Map<List<PermissionDto>>(entities);
		}
	}
}
