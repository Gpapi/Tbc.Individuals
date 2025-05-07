using MediatR;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Application.Features.Individuals.Commands.Delete;

public record DeleteIndividualCommand(int Id) : IRequest;

public class DeleteIndividualCommandHandler(IIndividualsRepository individualsRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteIndividualCommand>
{
    public async Task Handle(DeleteIndividualCommand request, CancellationToken cancellationToken)
    {
        await individualsRepository.DeleteAsync(request.Id, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
