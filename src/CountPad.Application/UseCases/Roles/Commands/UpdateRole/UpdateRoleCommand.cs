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

namespace CountPad.Application.UseCases.Roles.Commands.UpdateRole
{
	public class UpdateRoleCommand : IRequest<RoleDto>
	{
		public Guid Id { get; set; }
		public string RoleName { get; set; }
		public ICollection<Guid> Permissions { get; set; }
	}

	public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UpdateRoleCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
		{
			Role maybeRole = await _context.Roles.FindAsync(
				new object[] { request.Id }, cancellationToken);

			ValidateRoleIsNotNull(request, maybeRole);

			bool areAllExist = request.Permissions.All(
				x => _context.Permissions.Any(p => p.Id.Equals(x)));

			AreAllPermissionsExist(areAllExist);

			List<Permission> permissions =
				_context.Permissions.Where(p => request.Permissions.Contains(p.Id)).ToList();

			maybeRole.RoleName = request.RoleName;
			maybeRole.Permissions = permissions;

			maybeRole = _context.Roles.Update(maybeRole).Entity;

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

		private static void ValidateRoleIsNotNull(UpdateRoleCommand request, Role role)
		{
			if (role == null)
			{
				throw new NotFoundException(nameof(Role), request.Id);
			}
		}
	}
}
