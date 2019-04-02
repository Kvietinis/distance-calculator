using Distance.Contracts;
using FluentValidation;

namespace Distance.Api.Validators
{
    public class CoordinatesValidator : AbstractValidator<Coordinates>
    {
        public CoordinatesValidator()
        {
            RuleFor(c => c.From).NotNull();
            RuleFor(c => c.To).NotNull();
        }   
    }
}