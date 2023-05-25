using System;
using System.Threading;
using System.Threading.Tasks;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Domain.Entities.Identities;
using MediatR;

namespace CountPad.Application.UseCases.Permissions.Commands.DeletePermission
{
	public record DeletePermissionCommand(Guid permissionId): IRequest;

	public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand>
	{
		private readonly IApplicationDbContext _context;

		public DeletePermissionCommandHandler(IApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
		{
			Permission maybePermission = await _context.Permissions
				.FindAsync(new object[] { request.permissionId }, cancellationToken);

			if(maybePermission == null)
			{
				throw new NotFoundException(nameof(Permission), request.permissionId);
			}

			_context.Permissions.Remove(maybePermission);

			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
