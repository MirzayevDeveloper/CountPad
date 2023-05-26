using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Permissions.Models;
using CountPad.Application.UseCases.Roles.Models;
using CountPad.Domain.Entities.Identities;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Roles.Extensions
{
	public class RoleExtension
	{
		private static IApplicationDbContext _context;
		private static IMapper _mapper;

		public RoleExtension(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public static async ValueTask<RoleDto> GetRoleDtoFromDb(Role maybeRole)
		{
			List<RolePermission> rolePermissionsList = await
				_context.RolePermissions.Where(rp => rp.RoleId.Equals(
					maybeRole.Id)).ToListAsync();

			List<Guid> guids = rolePermissionsList
				.Select(r => r.PermissionId).ToList();

			IEnumerable<Permission> permissions =
				_context.GetByIds<Permission>(guids);

			PermissionDto[] dtos =
				_mapper.Map<List<PermissionDto>>(permissions).ToArray();

			return new RoleDto
			{
				RoleName = maybeRole.RoleName,
				Permissions = dtos
			};
		}
	}
}
