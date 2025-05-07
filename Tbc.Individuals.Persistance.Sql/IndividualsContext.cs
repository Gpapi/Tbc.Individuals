using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tbc.Individuals.Domain.Entities;
using Tbc.Individuals.Persistance.Sql.Seeding;

namespace Tbc.Individuals.Persistance.Sql;

public class IndividualsContext(DbContextOptions<IndividualsContext> options) : DbContext(options)
{
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<City> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.SeedData();

        base.OnModelCreating(modelBuilder);
    }
}
