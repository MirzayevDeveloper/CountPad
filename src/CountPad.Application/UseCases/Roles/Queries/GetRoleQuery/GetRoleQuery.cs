﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Roles.Models;
using CountPad.Domain.Entities.Identities;
using MediatR;

namespace CountPad.Application.UseCases.Roles.Queries.GetRoleQuery
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
				.FindAsync(new object[] { request.roleId }, cancellationToken);

			ValidateRoleIsNotNull(request, maybeRole);

			return _mapper.Map<RoleDto>(maybeRole);
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
