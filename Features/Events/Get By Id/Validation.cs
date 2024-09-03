using FluentValidation;

namespace EventAPI.Features.Events
{
    public class GetEventByIdValidator : AbstractValidator<GetEventById>
    {
        public GetEventByIdValidator()
        {
            RuleFor(e => e.EventId)
                .NotEmpty().WithMessage("Event ID cannot be empty.")
                .GreaterThan(0).WithMessage("Event ID must be greater than 0.");
        }
    }
}
