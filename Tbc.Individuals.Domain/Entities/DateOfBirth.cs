
using Tbc.Individuals.Domain.Utils;

namespace Tbc.Individuals.Domain.Entities;

public class DateOfBirth : ValueObject
{
    private DateOfBirth(DateOnly value)
    {
        Value = value;
    }

    public static DateOfBirth Create(DateOnly value)
    {
        var minDate = DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-18);
        if (value > minDate)
        {
            throw new DomainValidationException("Date of birth must be at least 18 years ago.");
        }
        return new DateOfBirth(value);
    }

    public DateOnly Value { get; }

    public static implicit operator DateOnly(DateOfBirth dob) => dob.Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        return [Value];
    }
}
