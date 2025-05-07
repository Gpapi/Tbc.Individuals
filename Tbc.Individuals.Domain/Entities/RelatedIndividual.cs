namespace Tbc.Individuals.Domain.Entities;

public class RelatedIndividual
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private RelatedIndividual() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public RelatedIndividual(RelationshipTypes relationshipType, Individual individual)
    {
        RelationshipType = relationshipType;
        Individual = individual ?? throw new ArgumentNullException(nameof(individual));
    }

    public int Id { get; }
    public RelationshipTypes RelationshipType { get; }
    public Individual Individual { get; }
}

public enum RelationshipTypes
{
    Parent = 1,
    Child = 2,
    Sibling = 3,
    Spouse = 4,
    Partner = 5,
    Other = 6
}