using CountPad.Application.Common.Models;

namespace CountPad.Application.UseCases.Permissions.Models
{
	public class PermissionDto : BaseAuditableEntityDto
	{
		public string PermissionName { get; set; }
	}
}
