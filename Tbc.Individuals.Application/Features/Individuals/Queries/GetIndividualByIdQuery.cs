using MediatR;
using Tbc.Individuals.Application.Helpers;
using Tbc.Individuals.Domain.Entities;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Application.Features.Individuals.Queries;

public record GetIndividualByIdQuery(int Id) : IRequest<GetIndividualByIdResponse?>;

public class GetIndividualByIdQueryHandler(IIndividualsRepository individualsRepository, LocalizationHelper localization) 
    : IRequestHandler<GetIndividualByIdQuery, GetIndividualByIdResponse?>
{
    public async Task<GetIndividualByIdResponse?> Handle(GetIndividualByIdQuery request, CancellationToken cancellationToken)
    {
        var individual = await individualsRepository.GetByIdAsync(request.Id, cancellationToken);

        if (individual is null)
        {
            return null;
        }

        return new GetIndividualByIdResponse(individual.Id, individual.FirstName.Value, individual.LastName.Value, individual.PersonalId.Value, individual.Gender,
            individual.DateOfBirth.Value, individual.City.Name.GetTranslatedValue(localization.CurrentCulture, localization.DefaultCulture),
            [.. individual.PhoneNumbers.Select(_ => new PhoneNumber(_.Number, _.Type))],
            [.. individual.RelatedIndividuals.Select(_ => new RelatedIndividual(_.Individual.Id, _.Individual.FirstName.ToString(), 
                _.Individual.LastName.ToString(), _.Individual.PersonalId.ToString(), _.RelationshipType))], 
            individual.ProfileImage);
    }
}

public record GetIndividualByIdResponse(int Id, string FirstName, string LastName, string PersonalId, Genders Gender, DateOnly DateOfBirth,
    string City, List<PhoneNumber> PhoneNumbers, List<RelatedIndividual> RelatedIndividuals, string? ProfileImage);

public record PhoneNumber(string Number, PhoneTypes Type);

public record RelatedIndividual(int Id, string FirstName, string LastName, string PersonalId, 
    RelationshipTypes Relationship);
