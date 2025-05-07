using Tbc.Individuals.Domain.Entities;

namespace Tbc.Individuals.Domain.Repositories;

public interface IIndividualsRepository
{
    Task<Individual?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Individual individual, CancellationToken cancellationToken = default);
    Task UpdateAsync(Individual individual, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<(List<Individual> Data, int Count)> Filter(string? searchTerm, DateOnly? birthDateFrom, DateOnly? birthDateTo,
        Genders? gender, int? city, int pageSize, int pageNumber);
}
