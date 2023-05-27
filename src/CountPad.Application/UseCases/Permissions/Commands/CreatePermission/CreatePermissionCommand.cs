using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Permissions.Models;
using CountPad.Domain.Entities.Identities;
using MediatR;

namespace CountPad.Application.UseCases.Permissions.Commands.CreatePermission
{
	public class CreatePermissionCommand : IRequest<PermissionDto>
	{
		private string _permissionName;

		public string PermissionName
		{
			get { return _permissionName; }
			set { _permissionName = value.ToLower(); }
		}
	}

	public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, PermissionDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CreatePermissionCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
		{
			Permission maybePermission = _context.Permissions
				.SingleOrDefault(p => p.PermissionName.Equals(request.PermissionName));

			ValidatePermissionDoesNoteExist(request, maybePermission);

			maybePermission = _context.Permissions.Add(new()
			{
				PermissionName = request.PermissionName
			}).Entity;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<PermissionDto>(maybePermission);
		}

		private static void ValidatePermissionDoesNoteExist(CreatePermissionCommand request, Permission permission)
		{
			if (permission != null)
			{
				throw new AlreadyExistsException(nameof(Permission), request.PermissionName);
			}
		}
	}
}
