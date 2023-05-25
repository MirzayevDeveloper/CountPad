using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Permissions.Models;
using CountPad.Domain.Entities.Identities;
using MediatR;

namespace CountPad.Application.UseCases.Permissions.Queries.GetPermission
{
	public record GetPermissionQuery(Guid permissionId) : IRequest<PermissionDto>;

	public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, PermissionDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetPermissionQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PermissionDto> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
		{
			Permission maybePermission =
				await _context.Permissions.FindAsync(request.permissionId);

			ValidatePermissionIsNotNull(request, maybePermission);

			return _mapper.Map<PermissionDto>(maybePermission);
		}

		private static void ValidatePermissionIsNotNull(GetPermissionQuery request, Permission maybePermission)
		{
			if (maybePermission == null)
			{
				throw new NotFoundException(nameof(Permission), request.permissionId);
			}
		}
	}
}
