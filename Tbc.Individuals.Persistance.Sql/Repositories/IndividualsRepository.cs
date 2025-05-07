using Microsoft.EntityFrameworkCore;
using Tbc.Individuals.Domain.Entities;
using Tbc.Individuals.Domain.Repositories;

namespace Tbc.Individuals.Persistance.Sql.Repositories;

public class IndividualsRepository(IndividualsContext context) : IIndividualsRepository
{
    public Task AddAsync(Individual individual, CancellationToken cancellationToken = default)
    {
        context.Individuals.Add(individual);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var individual = context.Individuals.Find(id);
        if (individual != null)
        {
            context.Individuals.Remove(individual);
        }
        return Task.CompletedTask;
    }

    public Task<Individual?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return context.Individuals
            .Include(i => i.City)
            .ThenInclude(c => c.Name)
            .ThenInclude(n => n.TranslationValues)
            .Include(r => r.RelatedIndividuals)
            .ThenInclude(i => i.Individual)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<(List<Individual> Data, int Count)> Filter(
        string? searchTerm,
        DateOnly? birthDateFrom,
        DateOnly? birthDateTo,
        Genders? gender,
        int? city,
        int pageSize,
        int pageNumber)
    {
        var query = context.Individuals.Include(_ => _.City).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(i => i.FirstName.ToString().Contains(searchTerm) ||
                                     i.LastName.ToString().Contains(searchTerm) || 
                                     i.PersonalId.ToString().Contains(searchTerm));
        }

        if (birthDateFrom.HasValue)
        {
            query = query.Where(i => i.DateOfBirth >= birthDateFrom.Value);
        }
        if (birthDateTo.HasValue)
        {
            query = query.Where(i => i.DateOfBirth <= birthDateTo.Value);
        }

        if (gender.HasValue)
        {
            query = query.Where(i => i.Gender == gender.Value);
        }

        if (city.HasValue)
        {
            query = query.Where(i => i.City.Id == city.Value);
        }

        var totalCount = await query.CountAsync();

        var data = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (data, totalCount);
    }

    public Task UpdateAsync(Individual individual, CancellationToken cancellationToken = default)
    {
        context.Individuals.Update(individual);
        return Task.CompletedTask;
    }
}
