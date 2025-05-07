using Microsoft.EntityFrameworkCore;
using Tbc.Individuals.Domain.Entities;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Persistance.Sql.Repositories;

public class CitiesRepository(IndividualsContext context) : ICitiesRepository
{
    public Task<List<City>> GetAllAsync(CancellationToken cancellationToken = default) => context.Cities
            .Include(c => c.Name)
            .ThenInclude(c => c.TranslationValues)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public Task<City?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => context.Cities
            .Include(c => c.Name)
            .ThenInclude(c => c.TranslationValues)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
}
