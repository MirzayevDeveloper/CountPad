using FluentValidation;

namespace CountPad.Application.UseCases.Distributors.Commands.CreateDistributor
{
	public class CreateDistributorCommandValidator : AbstractValidator<CreateDistributorCommand>
	{
		public CreateDistributorCommandValidator()
		{
			RuleFor(d => d.Name).NotEmpty()
				.NotEqual((string)default).WithMessage("Name is required.");

			RuleFor(d => d.DelivererPhone)
				.Must(ValidatePhone)
				.Length(13).WithMessage("Please enter valid phone number like +998901234567");

			RuleFor(d => d.CompanyPhone)
				.Must(ValidatePhone)
				.Length(13).WithMessage("Please enter valid phone number like +998901234567");
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
