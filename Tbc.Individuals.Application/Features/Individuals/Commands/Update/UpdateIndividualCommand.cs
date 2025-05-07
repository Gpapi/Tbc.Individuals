using MediatR;
using Tbc.Individuals.Application.Helpers;
using Tbc.Individuals.Domain.Entities;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Application.Features.Individuals.Commands.Update;

public record UpdateIndividualCommand(int Id, string FirstName, string LastName, Genders Gender, string PersonalId, DateOnly DateOfBirth, int City,
    List<(string Number, PhoneTypes Type)> PhoneNumbers) : IRequest;

public class UpdateIndividualCommandHandler(IIndividualsRepository individualsRepository, ICitiesRepository citiesRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateIndividualCommand>
{
    public async Task Handle(UpdateIndividualCommand request, CancellationToken cancellationToken)
    {
        var individual = await individualsRepository.GetByIdAsync(request.Id, cancellationToken);

        if (individual is null)
        {
            throw new NotFoundException("Individual not found");
        }

        if(individual.City.Id != request.City)
        {
            var city = await citiesRepository.GetByIdAsync(request.City, cancellationToken);
            if (city is null)
            {
                throw new ApplicationValidationException(Resources.Resources.CityNotFound);
            }
            individual.City = city;
        }

        individual.FirstName = FirstName.Create(request.FirstName);
        individual.LastName = LastName.Create(request.LastName);
        individual.Gender = request.Gender;
        individual.DateOfBirth = DateOfBirth.Create(request.DateOfBirth);
        individual.PersonalId = PersonalId.Create(request.PersonalId);
        individual.SetPhoneNumbers(request.PhoneNumbers);

        await individualsRepository.UpdateAsync(individual, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
