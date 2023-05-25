using System;
using MediatR;

namespace CountPad.Application.UseCases.Permissions.Commands.UpdatePermission
{
	public class UpdatePermissionCommand : IRequest
	{
        public Guid Id { get; set; }
		public string PermissionName { get; set; }
    }
}
