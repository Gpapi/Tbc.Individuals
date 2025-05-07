using MediatR;
using Tbc.Individuals.Application.Helpers;
using Tbc.Individuals.Domain.Entities;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Application.Features.Individuals.Commands.Create;

public record CreateIndividualCommand(string FirstName, string LastName, Genders Gender, string PersonalId, DateOnly DateOfBirth, int City, 
    List<(string Number, PhoneTypes Type)> PhoneNumbers) : IRequest<int>;

public class CreateIndividualCommandHandler(IIndividualsRepository individualsRepository, ICitiesRepository citiesRepository, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateIndividualCommand, int>
{
    public async Task<int> Handle(CreateIndividualCommand request, CancellationToken cancellationToken)
    {
        var city = await citiesRepository.GetByIdAsync(request.City, cancellationToken);

        if (city is null)
        {
            throw new ApplicationValidationException(Resources.Resources.CityNotFound);
        }

        var individual = new Individual(request.FirstName, request.LastName, request.Gender, request.DateOfBirth, request.PersonalId, city, request.PhoneNumbers);

        await individualsRepository.AddAsync(individual, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return individual.Id;
    }
}