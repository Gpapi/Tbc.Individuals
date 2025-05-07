using MediatR;
using Tbc.Individuals.Application.Helpers;
using Tbc.Individuals.Domain.Entities;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Application.Features.Individuals.Commands.Update;

public record AddRelatedIndividualCommand(int Id, int RelatedId, RelationshipTypes RelationshipType) : IRequest;

public class AddRelatedIndividualCommandHandler(IIndividualsRepository repository, IUnitOfWork unitOfWork) 
    : IRequestHandler<AddRelatedIndividualCommand>
{
    public async Task Handle(AddRelatedIndividualCommand request, CancellationToken cancellationToken)
    {
        var individual = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (individual == null)
        {
            throw new NotFoundException("Individual not found");
        }

        var relatedIndividual = await repository.GetByIdAsync(request.RelatedId, cancellationToken);

        if (relatedIndividual == null)
        {
            throw new NotFoundException("Related individual not found");
        }

        individual.AddRelatedIndividual(request.RelationshipType, relatedIndividual);

        await repository.UpdateAsync(individual, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
