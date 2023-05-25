using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Permissions.Models;
using CountPad.Domain.Entities.Identities;
using MediatR;

namespace CountPad.Application.UseCases.Permissions.Commands.DeletePermission
{
	public record DeletePermissionCommand(Guid permissionId): IRequest<PermissionDto>;

	public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, PermissionDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public DeletePermissionCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PermissionDto> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
		{
			Permission maybePermission = await _context.Permissions
				.FindAsync(new object[] { request.permissionId }, cancellationToken);

			if(maybePermission == null)
			{
				throw new NotFoundException(nameof(Permission), request.permissionId);
			}

			_context.Permissions.Remove(maybePermission);

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<PermissionDto>(maybePermission);
		}
	}
}
