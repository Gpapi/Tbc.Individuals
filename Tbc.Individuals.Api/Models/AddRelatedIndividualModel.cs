using Tbc.Individuals.Domain.Entities;

namespace Tbc.Individuals.Api.Models;

public record AddRelatedIndividualModel(int Id, RelationshipTypes RelationshipType);
