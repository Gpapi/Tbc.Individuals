using Tbc.Individuals.Domain.Utils;

namespace Tbc.Individuals.Domain.Entities;

public class PhoneNumber : ValueObject
{
    public string Number { get; }
    public PhoneTypes Type { get; }

    private PhoneNumber(string number, PhoneTypes type)
    {
        Number = number;
        Type = type;
    }

    public static PhoneNumber Create(string number, PhoneTypes type)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new DomainValidationException("Phone number cannot be empty.");

        var digitsOnly = number.Where(char.IsDigit).ToArray();
        if (digitsOnly.Length < 4 || digitsOnly.Length > 50)
            throw new DomainValidationException("Phone number must contain between 4 and 50 digits.");

        return new PhoneNumber(number, type);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Number;
        yield return Type;
    }

    public override string ToString() => $"{Type}: {Number}";
}

public enum PhoneTypes
{
    Mobile = 1,
    Home = 2,
    Work = 3,
    Other = 4
}
