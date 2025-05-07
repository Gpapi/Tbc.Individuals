namespace Tbc.Individuals.Domain.Entities;

public class City
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private City() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public City(int id, Translation name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public Translation Name { get; }
}
