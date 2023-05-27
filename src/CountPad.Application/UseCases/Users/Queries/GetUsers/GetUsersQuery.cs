using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CountPad.Application.Common.Interfaces;
using CountPad.Application.UseCases.Users.Models;
using CountPad.Domain.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CountPad.Application.UseCases.Users.Queries.GetUsers
{
	public record GetUsersQuery : IRequest<UserDto[]>;

	public class GetUserQueryHandler : IRequestHandler<GetUsersQuery, UserDto[]>
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

		public async Task<UserDto[]> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			User[] users = await _context.Users.ToArrayAsync();

			return _mapper.Map<UserDto[]>(users);
		}
	}
}
