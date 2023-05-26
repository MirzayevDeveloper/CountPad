using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Roles.Models;
using CountPad.Domain.Entities.Identities;
using MediatR;

namespace CountPad.Application.UseCases.Roles.Commands
{
	public record DeleteRoleCommand(Guid roleId) : IRequest<RoleDto>;

	public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, RoleDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public DeleteRoleCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<RoleDto> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
		{
			Role maybeRole =
				_context.Roles.Find(new object[] { request.roleId });

			ValidateRoleIsNotNull(request, maybeRole);

			_context.Roles.Remove(maybeRole);

			await _context.SaveChangesAsync(cancellationToken);

			return null;
		}

		private static void ValidateRoleIsNotNull(DeleteRoleCommand request, Role maybeRole)
		{
			if (maybeRole == null)
			{
				throw new NotFoundException(nameof(Role), request.roleId);
			}
		}
	}
}
