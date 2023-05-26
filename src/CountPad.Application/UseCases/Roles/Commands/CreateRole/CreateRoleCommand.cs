using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Roles.Models;
using CountPad.Domain.Entities.Identities;
using MediatR;

namespace CountPad.Application.UseCases.Roles.Commands.CreateRole
{
	public class CreateRoleCommand : IRequest<RoleDto>
	{
		public string RoleName { get; set; }

		public ICollection<Guid> Permissions { get; set; }
	}

	public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CreateRoleCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
		{
			Role maybeRole = _context.Roles
				.SingleOrDefault(r => r.RoleName.Equals(request));

			ValidateRoleIsNotExists(request, maybeRole);

			bool areAllExist = request.Permissions.All(
				x => _context.Permissions.Any(p => p.Id.Equals(x)));

			AreAllPermissionsExist(areAllExist);

			List<Permission> permissions =
				_context.Permissions.Where(p => request.Permissions.Contains(p.Id)).ToList();

			var entity = new Role
			{
				RoleName = request.RoleName,
				Permissions = permissions,
			};

			maybeRole = _context.Roles.Add(entity).Entity;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<RoleDto>(maybeRole);
		}

		private static void AreAllPermissionsExist(bool areAllExist)
		{
			if (!areAllExist)
			{
				throw new NotFoundException("Permission id does not exists");
			}
		}

		private static void ValidateRoleIsNotExists(CreateRoleCommand request, Role maybeRole)
		{
			if (maybeRole is not null)
			{
				throw new AlreadyExistsException(nameof(maybeRole), request.RoleName);
			}
		}

		private static void ValidateRoleIsNotNull(Guid requestId, Permission maybePermission)
		{
			if (maybePermission is null)
			{
				throw new NotFoundException(nameof(Permission), requestId);
			}
		}
	}

}
