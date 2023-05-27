using CountPad.Domain.Common.BaseEntities;

namespace CountPad.Domain.Common.Security
{
	public class UserToken : BaseEntity
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}
}
