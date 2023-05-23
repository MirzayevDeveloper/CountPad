using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using MediatR;

namespace CountPad.Application.UseCases.Permissions.Commands.CreatePermission
{
	public class CreatePermissionCommand : IRequest<int>
	{
		private string _permissionName;

		public string PermissionName
		{
			get { return _permissionName; }
			set { _permissionName = value.ToLower(); }
		}
	}

	public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, int>
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

		public async Task<int> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
		{
			_context.Permissions.Add(new()
			{
				PermissionName = request.PermissionName
			});

			return await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
