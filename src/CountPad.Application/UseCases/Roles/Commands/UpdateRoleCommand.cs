using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Roles.Models;
using CountPad.Domain.Entities.Identities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Roles.Commands
{
	public class UpdateRoleCommand : IRequest<RoleDto>
	{
        public Guid Id { get; set; }
        public string RoleName { get; set; }
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

			return _mapper.Map<RoleDto>(maybeRole);
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
