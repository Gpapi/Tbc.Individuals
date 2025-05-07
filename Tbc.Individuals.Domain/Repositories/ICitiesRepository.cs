using Tbc.Individuals.Domain.Entities;

namespace Tbc.Individuals.Domain.Repositories;

public interface ICitiesRepository
{
    Task<List<City>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<City?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
