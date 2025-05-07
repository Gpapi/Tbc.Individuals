using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Persistance.Sql.Repositories;

public class UnitOfWork(IndividualsContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);
}
