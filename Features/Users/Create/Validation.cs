using EventAPI.model;
using FluentValidation;

namespace EventAPI.Features.Users.Create
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(4).WithMessage("Name must be min 4 characters.");

        }
    }
}
