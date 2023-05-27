using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CountPad.Application.Common.Interfaces;
using CountPad.Domain.Common.Security;
using CountPad.Domain.Entities.Tokens;
using CountPad.Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CountPad.Application.UseCases.Authorizations.RefreshToken
{
	public class RefreshTokenCommand : IRequest<UserToken>
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public Guid Id { get; set; }
	}

	public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, UserToken>
	{
		private readonly IApplicationDbContext _context;
		private readonly ISecurityService _securityService;
		private readonly TokenConfiguration _tokenConfiguration;

		public RefreshTokenCommandHandler(
			IApplicationDbContext context,
			IConfiguration configuration,
			ISecurityService securityService)
		{
			_context = context;
			_securityService = securityService;
			_currentDateTime = DateTime.Now;

			_tokenConfiguration = new TokenConfiguration();
			configuration.Bind("Jwt", _tokenConfiguration);
		}

		private DateTimeOffset accessTokenExpire { get; set; }
		private DateTimeOffset refreshTokenExpire { get; set; }

		private DateTimeOffset _currentDateTime;
		private DateTimeOffset currentDateTime => _currentDateTime;


		public async Task<UserToken> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			User maybeUser = await _context.Users
				.FindAsync(new object[] { request.Id }, cancellationToken);

			if (maybeUser == null) return null;

			UserRefreshToken maybeRefreshToken =
				 _context.RefreshTokens.SingleOrDefault(r => r.Phone.Equals(maybeUser.Phone));

			ValidateRefreshToken(maybeRefreshToken, request.RefreshToken);

			try
			{
				UserToken userToken = await ValidateAndUpdateUserRefreshToken(maybeRefreshToken);

				return userToken;
			}
			finally
			{
				await _context.SaveChangesAsync(cancellationToken);
			}
		}

		private async ValueTask<UserToken> ValidateAndUpdateUserRefreshToken(UserRefreshToken refreshToken)
		{
			accessTokenExpire = refreshToken.AccessTokenExpiredDateTime;

			refreshTokenExpire = refreshToken.RefreshTokenExpiredDateTime;

			if (currentDateTime >= refreshTokenExpire)
			{
				_context.RefreshTokens.Remove(refreshToken);
				return null;
			}
			else if (currentDateTime >= accessTokenExpire)
			{
				UserToken userToken = await GetUserTokenByUser(refreshToken);

				if (userToken is null) return null;

				CreateRefreshToken(refreshToken, userToken);

				return userToken;
			}

			return new UserToken();
		}

		private void CreateRefreshToken(UserRefreshToken refreshToken, UserToken userToken)
		{
			accessTokenExpire = currentDateTime
				.AddMinutes(_tokenConfiguration.AccessTokenExpires);

			refreshTokenExpire = currentDateTime
				.AddMinutes(_tokenConfiguration.RefreshTokenExpires);

			UserRefreshToken updatedToken = _context.RefreshTokens
				.SingleOrDefault(r => r.Id.Equals(refreshToken.Id));

			updatedToken.RefreshToken = userToken.RefreshToken;
			updatedToken.AccessTokenExpiredDateTime = accessTokenExpire;
			updatedToken.RefreshTokenExpiredDateTime = refreshTokenExpire;
		}

		private async Task<UserToken> GetUserTokenByUser(UserRefreshToken refreshToken)
		{
			User user = await _context.Users
								.SingleOrDefaultAsync(u => u.Phone.Equals(refreshToken.Phone));

			UserToken userToken = CreateUserToken(user);

			return userToken;
		}

		private UserToken CreateUserToken(User maybeUser)
		{
			string token = _securityService.GenerateJWT(maybeUser);
			string refreshToken = _securityService.GenerateRefreshToken();

			return new()
			{
				Id = maybeUser.Id,
				AccessToken = token,
				RefreshToken = refreshToken,
			};
		}

		private static void ValidateRefreshToken(UserRefreshToken maybeRefreshToken, string refreshToken)
		{
			if (maybeRefreshToken == null || !maybeRefreshToken.RefreshToken.Equals(refreshToken))
			{
				throw new UnauthorizedAccessException("Invalid attempt!");
			}
		}
	}
}
