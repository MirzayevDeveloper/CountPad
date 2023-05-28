using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Permissions.Models;
using CountPad.Domain.Entities.Identities;
using MediatR;

namespace CountPad.Application.UseCases.Permissions.Commands.UpdatePermission
{
	public class UpdatePermissionCommand : IRequest<PermissionDto>
	{
		private string _permissionName;

		public Guid Id { get; set; }

		public string PermissionName
		{
			get { return _permissionName; }
			set { _permissionName = value.ToLower(); }
		}
	}

	public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, PermissionDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public UpdatePermissionCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PermissionDto> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
		{
			Permission maybePermission = await _context.Permissions
				.FindAsync(new object[] { request.Id }, cancellationToken);

			ValidatePermissionIsNotNull(request, maybePermission);

			maybePermission.PermissionName = request.PermissionName;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<PermissionDto>(maybePermission);
		}

		private static void ValidatePermissionIsNotNull(UpdatePermissionCommand request, Permission maybePermission)
		{
			if (maybePermission == null)
			{
				throw new NotFoundException(nameof(Permission), request.Id);
			}
		}
	}
}
