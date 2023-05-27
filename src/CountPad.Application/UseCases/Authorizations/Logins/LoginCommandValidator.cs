using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CountPad.Application.UseCases.Authorizations.Logins
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(u => u.Phone)
                .Must(ValidatePhone)
                .Length(13);
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
