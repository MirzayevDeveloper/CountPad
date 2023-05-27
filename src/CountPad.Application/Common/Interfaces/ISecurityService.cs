using System.Threading.Tasks;
using CountPad.Domain.Entities.Users;

namespace CountPad.Application.Common.Interfaces
{
	public interface ISecurityService
	{
		string GenerateJWT(User user);
		string GenerateRefreshToken();
		string HashToken(string password);
	}
}
