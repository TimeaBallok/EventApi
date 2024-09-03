using EventAPI.model;
using FluentValidation;

namespace EventAPI.validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() 
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be min 3 characters.");

        }
    }
}
