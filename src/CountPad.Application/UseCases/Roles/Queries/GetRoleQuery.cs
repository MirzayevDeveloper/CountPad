using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Permissions.Models;
using CountPad.Application.UseCases.Roles.Extensions;
using CountPad.Application.UseCases.Roles.Models;
using CountPad.Domain.Entities.Identities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Roles.Queries
{
	public record GetRoleQuery(Guid roleId) : IRequest<RoleDto>;

	public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, RoleDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetRoleQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<RoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
		{
			Role maybeRole = await _context.Roles
				.FindAsync(new object[] { request.roleId });

			ValidateRoleIsNotNull(request, maybeRole);

			RoleDto dto = await RoleExtension.GetRoleDtoFromDb(maybeRole);

			return dto;
		}

		private static void ValidateRoleIsNotNull(GetRoleQuery request, Role maybeRole)
		{
			if (maybeRole == null)
			{
				throw new NotFoundException(nameof(Role), request.roleId);
			}
		}
	}
}
