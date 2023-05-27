using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Users.Models;
using CountPad.Domain.Entities.Users;
using MediatR;

namespace CountPad.Application.UseCases.Users.Commands.DeleteUser
{
	public record DeleteUserCommand(Guid userId) : IRequest<UserDto>;

	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public DeleteUserCommandHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<UserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			User maybeUser = await _context.Users
				.FindAsync(new object[] { request.userId }, cancellationToken);

			ValidateUserIsNotNull(request, maybeUser);

			maybeUser = _context.Users.Remove(maybeUser).Entity;

			return _mapper.Map<UserDto>(maybeUser);
		}

		private static void ValidateUserIsNotNull(DeleteUserCommand request, User maybeUser)
		{
			if (maybeUser == null)
			{
				throw new NotFoundException(nameof(User), request.userId);
			}
		}
	}
}
