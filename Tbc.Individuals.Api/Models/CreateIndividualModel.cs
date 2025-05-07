#nullable disable

using Tbc.Individuals.Domain.Entities;

namespace Tbc.Individuals.Api.Models;

public record CreateIndividualModel
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string PersonalId { get; init; }
    public Genders Gender { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public int City { get; init; }
    public List<PhoneNumberModel> PhoneNumbers { get; init; }
}
