using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Domain.Common.Security;
using CountPad.Domain.Entities.Tokens;
using CountPad.Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace CountPad.Application.UseCases.Authorizations.Logins
{
    public class LoginCommand : IRequest<UserToken>
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserToken>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISecurityService _securityService;
        private readonly TokenConfiguration _tokenConfiguration;

        public LoginCommandHandler(
            IApplicationDbContext context,
            ISecurityService securityService,
            IConfiguration configuration)
        {
            _context = context;
            _securityService = securityService;
            _tokenConfiguration = new TokenConfiguration();
            configuration.Bind("Jwt", _tokenConfiguration);
        }

        public DateTime CurrentDateTime => DateTime.UtcNow;

        public async Task<UserToken> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User maybeUser = GetUserByCredentials(request);

            UserRefreshToken refreshToken = GetUserRefreshTokenByPhoneNumber(maybeUser.Phone);

            UserToken userToken = CreateUserToken(maybeUser);

            refreshToken = refreshToken is null ? UserCreateRefreshToken(maybeUser, userToken)
                                                : UpdateUserRefreshToken(refreshToken, userToken);

            await _context.SaveChangesAsync(cancellationToken);

            return userToken;
        }

        private UserRefreshToken UpdateUserRefreshToken(UserRefreshToken refreshToken, UserToken userToken)
        {
            DateTime accessTokenExpires =
                CurrentDateTime.AddMinutes(_tokenConfiguration.AccessTokenExpires);

            refreshToken.AccessTokenExpiredDateTime = accessTokenExpires;

            refreshToken.RefreshToken = userToken.RefreshToken;

            return _context.RefreshTokens.Update(refreshToken).Entity;
        }

        private UserRefreshToken UserCreateRefreshToken(User maybeUser, UserToken userToken)
        {
            DateTime currentDateTime = CurrentDateTime;

            DateTime accessTokenExpires =
                currentDateTime.AddMinutes(_tokenConfiguration.AccessTokenExpires);

            DateTime refreshTokenExpires =
                currentDateTime.AddMinutes(_tokenConfiguration.RefreshTokenExpires);

            var refreshToken = new UserRefreshToken
            {
                Id = Guid.NewGuid(),
                Phone = maybeUser.Phone,
                RefreshToken = userToken.RefreshToken,
                AccessTokenExpiredDateTime = accessTokenExpires,
                RefreshTokenExpiredDateTime = refreshTokenExpires
            };

            return _context.RefreshTokens.Add(refreshToken).Entity;
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

        private UserRefreshToken GetUserRefreshTokenByPhoneNumber(string phone)
        {
            return _context.RefreshTokens.SingleOrDefault(r => r.Phone.Equals(phone));
        }

        private User GetUserByCredentials(LoginCommand request)
        {
            User maybeUser = _context.Users
                .ToList().First(u => u.Phone.Equals(request.Phone));

            if (maybeUser is null)
            {
                throw new NotFoundException(nameof(LoginCommand.Phone), request.Phone);
            }

            string hashedPassword =
                _securityService.GetHash(request.Password);

            if (maybeUser.Password != hashedPassword)
            {
                throw new NotFoundException("Incorrect Password");
            }

            return maybeUser;
        }
    }
}
