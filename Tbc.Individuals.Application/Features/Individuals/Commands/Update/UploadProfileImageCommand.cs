using MediatR;
using Tbc.Individuals.Application.Helpers;
using Tbc.Individuals.Domain;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Application.Features.Individuals.Commands.Update;

public record UploadProfileImageCommand(int Id, string FileName, Stream FileStream) : IRequest;

public class UploadProfileImageCommandHandler(IIndividualsRepository individualsRepository, IUnitOfWork unitOfWork, 
    IFileStorage fileStorage) : IRequestHandler<UploadProfileImageCommand>
{
    public async Task Handle(UploadProfileImageCommand request, CancellationToken cancellationToken)
    {
        var individual = await individualsRepository.GetByIdAsync(request.Id, cancellationToken);
        if (individual is null)
        {
            throw new NotFoundException("Individual not found");
        }

        var filePath = await fileStorage.Upload(request.FileStream, request.FileName, cancellationToken);

        individual.ProfileImage = filePath;
        await individualsRepository.UpdateAsync(individual, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
