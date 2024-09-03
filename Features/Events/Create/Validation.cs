using FluentValidation;

namespace EventAPI.Features.Events.Create
{
    public class CreateEventValidator : AbstractValidator<CreateEvent>
    {
        public CreateEventValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

            RuleFor(e => e.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(100).WithMessage("Location must not exceed 100 characters.");

            RuleFor(e => e.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
