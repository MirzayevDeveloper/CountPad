using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountPad.Application.UseCases.Users.Models;
using MediatR;

namespace CountPad.Application.UseCases.Users.Commands.CreateUser
{
	public class CreateUserCommand : IRequest<UserDto>
	{

	}
}
