using FluentValidation;

namespace Tbc.Individuals.Application.Features.Individuals.Commands.Update;

public class UploadProfileImageCommandValidator : AbstractValidator<UploadProfileImageCommand>
{
    public UploadProfileImageCommandValidator()
    {
        RuleFor(x => x.FileName)
            .NotEmpty()
            .WithMessage("File name is required.");
        RuleFor(x => x.FileStream)
            .NotNull()
            .WithMessage("File is required.");
    }
}
