using MediatR;
using Microsoft.Extensions.Logging;
using Tbc.Individuals.Application.Helpers;
using Tbc.Individuals.Domain;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Application.Features.Individuals.Commands.Update;

public record DeleteProfileImageCommand(int Id) : IRequest;

public class DeleteProfileImageCommandHandler(IIndividualsRepository individualsRepository, IUnitOfWork unitOfWork, 
    IFileStorage fileStorage, ILogger<DeleteProfileImageCommandHandler> logger) : IRequestHandler<DeleteProfileImageCommand>
{
    public async Task Handle(DeleteProfileImageCommand request, CancellationToken cancellationToken)
    {
        var individual = await individualsRepository.GetByIdAsync(request.Id, cancellationToken);
        if (individual is null)
        {
            throw new NotFoundException("Individual not found");
        }

        var image = individual.ProfileImage;

        if (string.IsNullOrWhiteSpace(image))
        {
            return;
        }

        individual.ProfileImage = null;
        await individualsRepository.UpdateAsync(individual, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        try
        {
            await fileStorage.Delete(image, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error deleting profile image for individual {Id}", request.Id);
        }
    }
}
