using FluentValidation;

namespace EventAPI.Features.Users
{
    public class GetUserByIdValidator : AbstractValidator<GetUserById>
    {
        public GetUserByIdValidator()
        {
            RuleFor(e => e.UserId)
                .NotEmpty().WithMessage("Event ID cannot be empty.")
                .GreaterThan(0).WithMessage("Event ID must be greater than 0.");
        }
    }
}
