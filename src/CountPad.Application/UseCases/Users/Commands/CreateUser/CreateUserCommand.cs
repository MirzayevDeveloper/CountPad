﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Users.Models;
using CountPad.Domain.Entities.Identities;
using CountPad.Domain.Entities.Users;
using MediatR;

namespace CountPad.Application.UseCases.Users.Commands.CreateUser
{
	public class CreateUserCommand : IRequest<UserDto>
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Password { get; set; }

		public ICollection<Guid> Roles { get; set; }
	}

	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public CreateUserCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			User maybeUser = _context.Users.Where(
				u => u.Phone.Equals(request.Phone)).FirstOrDefault();

			ValidateUserNotExists(request, maybeUser);

			bool areAllExist = request.Roles.All(
				x => _context.Roles.Any(p => p.Id.Equals(x)));

			ValidateAllRolesExist(areAllExist);

			List<Role> roles = _context.Roles
				.Where(r => request.Roles.Contains(r.Id)).ToList();

			var user = new User
			{
				Name = maybeUser.Name,
				Phone = maybeUser.Phone,
				Password = maybeUser.Password,
				Roles = roles
			};

			maybeUser = _context.Users.Add(user).Entity;

			await _context.SaveChangesAsync(cancellationToken);

			return _mapper.Map<UserDto>(maybeUser);
		}

		private static void ValidateUserNotExists(CreateUserCommand request, User maybeUser)
		{
			if (maybeUser != null)
			{
				throw new AlreadyExistsException(nameof(User), request.Phone);
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
