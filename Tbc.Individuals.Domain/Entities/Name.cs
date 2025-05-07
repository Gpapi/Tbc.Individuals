using Tbc.Individuals.Domain.Utils;

namespace Tbc.Individuals.Domain.Entities;

public abstract class Name : ValueObject
{
    public string Value { get; }

    protected Name(string value)
    {
        Value = value;
    }

    protected static bool IsValidFormat(string value)
    {
        if (value.StartsWith('-') || value.EndsWith('-') || value.Contains("--"))
            return false;

        bool hasLatin = false;
        bool hasGeorgian = false;

        foreach (var c in value)
        {
            if (c == '-') continue;

            if (c is >= 'A' and <= 'Z' or >= 'a' and <= 'z') hasLatin = true;
            else if (c >= 'ა' && c <= 'ჰ') hasGeorgian = true;
            else return false; // invalid char
        }

        return hasLatin ^ hasGeorgian; // xor: only one alphabet
    }

    internal AlphabetType GetAlphabetType()
    {
        bool hasLatin = Value.Any(c => char.IsLetter(c) && c is >= 'A' and <= 'Z' or >= 'a' and <= 'z');
        bool hasGeorgian = Value.Any(c => c >= 'ა' && c <= 'ჰ');
        if (hasLatin && !hasGeorgian) return AlphabetType.Latin;
        if (hasGeorgian && !hasLatin) return AlphabetType.Georgian;
        return AlphabetType.Mixed;
    }
}

public sealed class FirstName : Name
{
    private FirstName(string value) : base(value) { }

    public static FirstName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException($"First name is required.");

        if (value.Length < 2 || value.Length > 50)
            throw new DomainValidationException($"First name must be between 2 and 50 characters.");

        if (!IsValidFormat(value))
            throw new DomainValidationException($"First name must contain only Georgian or only Latin letters and may include hyphens.");

        return new FirstName(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}

public sealed class LastName : Name
{
    private LastName(string value) : base(value) { }

    public static LastName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainValidationException($"Last name is required.");

        if (value.Length < 2 || value.Length > 50)
            throw new DomainValidationException($"Last name must be between 2 and 50 characters.");

        if (!IsValidFormat(value))
            throw new DomainValidationException($"Last name must contain only Georgian or only Latin letters and may include hyphens.");

        return new LastName(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}

public enum AlphabetType
{
    Georgian,
    Latin,
    Mixed
}
