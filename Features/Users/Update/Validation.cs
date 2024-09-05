using EventAPI.Features.Events.Update;
using FluentValidation;

namespace EventAPI.Features.Users.Update
{
    public class UpdateUserValidator : AbstractValidator<UpdateUser>
    {
        public UpdateUserValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(4).WithMessage("Name must be min 4 characters.");
        }
    }
}
