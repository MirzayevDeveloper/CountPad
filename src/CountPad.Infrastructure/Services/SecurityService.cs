using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CountPad.Application.Common.Interfaces;
using CountPad.Domain.Common.Security;
using CountPad.Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CountPad.Infrastructure.Services
{
	public class SecurityService : ISecurityService
	{
		private readonly TokenConfiguration _tokenConfiguration;

		public SecurityService(IConfiguration configuration)
		{
			_tokenConfiguration = new TokenConfiguration();
			configuration.Bind("Jwt", _tokenConfiguration);
		}

		public string GenerateJWT(User user)
		{
			byte[] convertKeyToBytes =
				Encoding.UTF8.GetBytes(_tokenConfiguration.Key);

			var securityKey =
				new SymmetricSecurityKey(convertKeyToBytes);

			var credentials =
				new SigningCredentials(
					securityKey, SecurityAlgorithms.HmacSha256);

			var claimList = new List<Claim>();

			if (user.Roles != null)
			{
				foreach (var role in user.Roles)
				{
					foreach (var permission in role.Permissions)
					{
						claimList.Add(new Claim(ClaimTypes.Role, permission.PermissionName));
					}
				}
			}

			var claims = new Claim[]
			{
				new Claim(ClaimTypes.Name, user.Name),
				new Claim(ClaimTypes.MobilePhone, user.Phone),
				new Claim("Password", user.Password),
			};

			claimList.AddRange(claims);

			var token = new JwtSecurityToken(
				issuer: _tokenConfiguration.Issuer,
				audience: _tokenConfiguration.Audience,
				claims: claimList,
				expires: DateTime.UtcNow.AddMinutes(_tokenConfiguration.AccessTokenExpires),
				signingCredentials: credentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public string GenerateRefreshToken()
		{
			string key = GetHash(_tokenConfiguration.Key);
			string dateTime = GetHash(DateTime.UtcNow.ToString());
			string refreshToken = key + dateTime;

			return refreshToken;
		}

		public string GetHash(string password)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
			}
		}
	}
}
