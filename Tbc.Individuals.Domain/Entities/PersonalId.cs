namespace Tbc.Individuals.Domain.Entities;

public class PersonalId : ValueObject
{
    public string Value { get; }

    private PersonalId(string value)
    {
        Value = value;
    }

    public static PersonalId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Personal ID cannot be empty.", nameof(value));

        if (value.Length != 11 || !value.All(char.IsDigit))
            throw new ArgumentException("Personal ID must be an 11-digit number.", nameof(value));

        return new PersonalId(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
