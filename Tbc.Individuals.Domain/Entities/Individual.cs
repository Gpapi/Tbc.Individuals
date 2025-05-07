using Tbc.Individuals.Domain.Utils;

namespace Tbc.Individuals.Domain.Entities;

public class Individual
{
    private readonly List<PhoneNumber> _phoneNumbers;
    private readonly List<RelatedIndividual> _relatedIndividuals;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Individual() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Individual(
       string firstName,
       string lastName,
       Genders gender,
       DateOnly dateOfBirth,
       string personalId,
       City city,
       List<(string Number, PhoneTypes Type)> numbers,
       string? profileImage = null,
       List<RelatedIndividual>? relatedIndividuals = null)
    {
        FirstName = FirstName.Create(firstName);
        LastName = LastName.Create(lastName);

        if(FirstName.GetAlphabetType() != LastName.GetAlphabetType())
        {
            throw new DomainValidationException("First name and last name must be in the same alphabet.");
        }

        Gender = gender;
        DateOfBirth = DateOfBirth.Create(dateOfBirth);
        PersonalId = PersonalId.Create(personalId);
        City = city;
        ProfileImage = profileImage;
        
        _relatedIndividuals = relatedIndividuals ?? [];
        _phoneNumbers = [.. numbers.Select(n => PhoneNumber.Create(n.Number, n.Type))];
    }

    public int Id { get; private set; }
    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; }
    public Genders Gender { get; set; }
    public DateOfBirth DateOfBirth { get; set; }
    public PersonalId PersonalId { get; set; }
    public City City { get; set; }
    public IReadOnlyList<PhoneNumber> PhoneNumbers => _phoneNumbers;
    public string? ProfileImage { get; set; }
    public IReadOnlyList<RelatedIndividual> RelatedIndividuals => _relatedIndividuals;

    public void SetPhoneNumbers(List<(string Number, PhoneTypes Type)> phoneNumbers)
    {
        _phoneNumbers.Clear();
        _phoneNumbers.AddRange([.. phoneNumbers.Select(n => PhoneNumber.Create(n.Number, n.Type))]);
    }

    public void AddRelatedIndividual(RelationshipTypes relationshipType, Individual individual)
    {
        ArgumentNullException.ThrowIfNull(individual);

        if (Id == individual.Id)
        {
            throw new DomainValidationException("Cannot add self as related individual");
        }

        if (_relatedIndividuals.Any(x => x.Individual.Id == individual.Id))
        {
            throw new DomainValidationException("Related individual already exists.");
        }

        var relatedIndividual = new RelatedIndividual(relationshipType, individual);
        _relatedIndividuals.Add(relatedIndividual);
    }

    public void RemoveRelatedIndividual(int relatedId)
    {
        var relatedIndividual = _relatedIndividuals.FirstOrDefault(x => x.Individual.Id == relatedId);
        if (relatedIndividual != null)
        {
            _relatedIndividuals.Remove(relatedIndividual);
        }
    }
}