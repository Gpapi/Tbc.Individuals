using FluentValidation;

namespace Tbc.Individuals.Application.Features.Individuals.Commands.Create;

public class CreateIndividualCommandValidator : AbstractValidator<CreateIndividualCommand>
{
    public CreateIndividualCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(Resources.Resources.FirstNameRequired)
            .MinimumLength(2)
            .WithMessage(Resources.Resources.FirstNameMinLengthRequired)
            .MaximumLength(50)
            .WithMessage(Resources.Resources.FirstNameMaxLengthValidation);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(Resources.Resources.LastNameRequired)
            .MinimumLength(2)
            .WithMessage(Resources.Resources.LastNameMinLengthRequired)
            .MaximumLength(50)
            .WithMessage(Resources.Resources.LastNameMaxLengthValidation);

        RuleFor(x => x.Gender)
            .NotEmpty()
            .IsInEnum()
            .WithMessage(Resources.Resources.GenderValidation);

        RuleFor(x => x.PersonalId)
            .NotEmpty()
            .Matches(@"^[0-9]{11}$")
            .WithMessage(Resources.Resources.PersonalIdValidation);
    }
}
