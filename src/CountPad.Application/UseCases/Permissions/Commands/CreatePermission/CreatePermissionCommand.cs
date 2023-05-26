using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
		private readonly IDateTime dateTime;

		public CreatePermissionCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
		{
			var a = _context.Permissions.Add(new()
			{
				PermissionName = request.PermissionName
			}).Entity;

			await _context.SaveChangesAsync(cancellationToken);

			Permission permission = _context.Permissions
				.SingleOrDefault(p => p.PermissionName.Equals(request.PermissionName));

			return _mapper.Map<PermissionDto>(permission);
		}
	}
}
