using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CountPad.Application.UseCases.Users.Commands.UpdateUser
{
	public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserCommandValidator()
		{
			RuleFor(u => u.Name)
				.Must(NotBeWhitespace)
				.WithMessage("User name is required.");

			RuleFor(u => u.Phone)
				.Must(ValidatePhone)
				.Length(12).WithMessage("Please enter valid phone number like +998901234567");

			RuleFor(u => u.Password)
				.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
				.WithMessage("Password must be at least 8 characters long and contain at" +
				" least one uppercase letter, one lowercase letter, one digit, and one special character.")
				.Equal(u => u.ConfirmPassword)
				.WithMessage("ConfirmPassword not same with password");

			RuleFor(u => u.Roles)
				.ForEach(r => r.NotEqual((Guid)default))
				.WithMessage("Please enter valid role");
		}

		private bool NotBeWhitespace(string value)
		{
			return !string.IsNullOrWhiteSpace(value);
		}

		private bool ValidatePhone(string phone)
		{
			bool isTrue = phone.StartsWith("+998");

			for (int i = 1; i < phone.Length; i++)
			{
				if (!char.IsNumber(phone[i]))
				{
					phone.Remove(i, 1);
				}
			}

			return isTrue;
		}
	}
}
