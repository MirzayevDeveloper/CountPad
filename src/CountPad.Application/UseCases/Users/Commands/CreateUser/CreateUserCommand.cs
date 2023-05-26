using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.Common.Models;
using CountPad.Application.UseCases.Users.Models;
using CountPad.Domain.Entities.Identities;
using CountPad.Domain.Entities.Users;
using MediatR;

namespace CountPad.Application.UseCases.Users.Commands.CreateUser
{
	public class CreateUserCommand : IRequest<ResponseCore<UserDto>>
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public string Password { get; set; }

		public ICollection<Guid> Roles { get; set; }
	}

	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseCore<UserDto>>
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

		public async Task<ResponseCore<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			bool isExists = _context.Users.Any(u => u.Phone == request.Phone);

			ValidateUserNotExists(request, isExists);

			bool areAllExist = request.Roles.All(
				x => _context.Roles.Any(p => p.Id.Equals(x)));

			ValidateAllRolesExist(areAllExist);

			List<Role> roles = _context.Roles
				.Where(r => request.Roles.Contains(r.Id)).ToList();

			var user = new User
			{
				Name = request.Name,
				Phone = request.Phone,
				Password = request.Password,
				Roles = roles,
			};

			user = _context.Users.Add(user).Entity;

			await _context.SaveChangesAsync(cancellationToken);

			var result = new ResponseCore<UserDto>
			{
				IsSuccess = true,
				Result = _mapper.Map<UserDto>(user)
			};

			return result;
		}

		private static void ValidateUserNotExists(CreateUserCommand request, bool isExists)
		{
			if (isExists)
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
