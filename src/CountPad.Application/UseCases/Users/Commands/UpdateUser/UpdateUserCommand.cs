using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Users.Models;
using CountPad.Domain.Entities.Identities;
using CountPad.Domain.Entities.Users;
using MediatR;

namespace CountPad.Application.UseCases.Users.Commands.UpdateUser
{
	public class UpdateUserCommand : IRequest<UserDto>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public string CurrentPassword { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }

		public ICollection<Guid> Roles { get; set; }
	}

	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
	{
		private readonly IMapper _mapper;
		private readonly IApplicationDbContext _context;
		private readonly ISecurityService _securityService;

		public UpdateUserCommandHandler(
			IMapper mapper,
			IApplicationDbContext context,
			ISecurityService securityService)
		{
			_mapper = mapper;
			_context = context;
			_securityService = securityService;
		}

		public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			User maybeUser = await _context.Users
				.FindAsync(new object[] { request.Id }, cancellationToken);

			ValidateUserIsNotNull(request, maybeUser);

			string hashedRequestPassword =
				_securityService.GetHash(request.CurrentPassword);

			ValidatePasswordIsCorrect(maybeUser, hashedRequestPassword);

			bool areAllExist = request.Roles.All(
				x => _context.Roles.Any(p => p.Id.Equals(x)));

			ValidateAllRolesExist(areAllExist);

			List<Role> roles = _context.Roles
				.Where(r => request.Roles.Contains(r.Id)).ToList();

			maybeUser.Phone = request.Phone;
			maybeUser.Password = hashedRequestPassword;
			maybeUser.Roles = roles;
			maybeUser.Name = request.Name;

			maybeUser = _context.Users.Update(maybeUser).Entity;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<UserDto>(maybeUser);
		}

		private static void ValidatePasswordIsCorrect(User maybeUser, string hashedRequestPassword)
		{
			if (hashedRequestPassword != maybeUser.Password)
			{
				throw new ValidationException(
					nameof(UpdateUserCommand.CurrentPassword), "Old Password is incorrect.");
			}
		}

		private static void ValidateUserIsNotNull(UpdateUserCommand request, User maybeUser)
		{
			if (maybeUser == null)
			{
				throw new NotFoundException(nameof(User), request.Id);
			}
		}

		private static void ValidateAllRolesExist(bool areAllExist)
		{
			if (!areAllExist)
			{
				throw new NotFoundException("Role does not exist");
			}
		}
	}
}
