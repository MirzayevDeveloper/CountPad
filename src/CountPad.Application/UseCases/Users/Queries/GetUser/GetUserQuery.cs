using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Exceptions;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Users.Models;
using CountPad.Domain.Entities.Users;
using MediatR;

namespace CountPad.Application.UseCases.Users.Queries.GetUser
{
	public record GetUserQuery(Guid userId) : IRequest<UserDto>;

	public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetUserQueryHandler(
			IApplicationDbContext context,
			IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
		{
			User maybeUser = await _context.Users
				.FindAsync(new object[] { request.userId });

			ValidateUserIsNotNull(request, maybeUser);

			return _mapper.Map<UserDto>(maybeUser);
		}

		private static void ValidateUserIsNotNull(GetUserQuery request, User maybeUser)
		{
			if (maybeUser == null)
			{
				throw new NotFoundException(nameof(User), request.userId);
			}
		}
	}
}
