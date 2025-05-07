using MediatR;
using Tbc.Individuals.Application.Helpers;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Application.Features.Individuals.Commands.Update;

public record RemoveRelatedIndividualCommand(int Id, int RelatedId) : IRequest;

public class RemoveRelatedIndividualCommandHandler(IIndividualsRepository repository, IUnitOfWork unitOfWork) 
    : IRequestHandler<RemoveRelatedIndividualCommand>
{
    public async Task Handle(RemoveRelatedIndividualCommand request, CancellationToken cancellationToken)
    {
        var individual = await repository.GetByIdAsync(request.Id, cancellationToken);

        if(individual == null)
        {
            throw new NotFoundException("Individual not found");
        }

        individual.RemoveRelatedIndividual(request.RelatedId);

        await repository.UpdateAsync(individual, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}