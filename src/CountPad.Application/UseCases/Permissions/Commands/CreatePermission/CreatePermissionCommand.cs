using MediatR;

namespace CountPad.Application.UseCases.Permissions.Commands.CreatePermission
{
	public class CreatePermissionCommand : IRequest
	{
		private string _permissionName;

		public string PermissionName
		{
			get { return _permissionName; }
			set { _permissionName = value.ToLower(); }
		}
	}
}
