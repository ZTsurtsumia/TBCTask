using FluentValidation;
using PersonDirectory.Application.Dtos;

namespace PersonDirectory.Application.Validations
{
    public class AddPersonRequestValidator : AbstractValidator<AddPersonRequest>
    {
        public AddPersonRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MinimumLength(2).WithMessage("First Name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("First Name must not exceed 50 characters.")
                .Matches("^[\\u10A0-\\u10FF]+$|^[A-Za-z]+$").WithMessage("First Name must contain only Georgian or only Latin letters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MinimumLength(2).WithMessage("Last Name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Last Name must not exceed 50 characters.")
                .Matches("^[\\u10A0-\\u10FF]+$|^[A-Za-z]+$").WithMessage("Last Name must contain only Georgian or only Latin letters.");

            RuleFor(x => x.Sex)
                .IsInEnum().WithMessage("Invalid Sex value.");

            RuleFor(x => x.PersonalN)
                .NotEmpty().WithMessage("Personal Number is required.")
                .Length(11).WithMessage("Personal Number must be exactly 11 digits.")
                .Matches("^[0-9]+$").WithMessage("Personal Number must contain only digits.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.")
                .Must(BeAtLeast18YearsOld).WithMessage("Person must be at least 18 years old.");
        }

        private bool BeAtLeast18YearsOld(DateTime birthDate)
        {
            return birthDate <= DateTime.UtcNow.AddYears(-18);
        }
    }
}
