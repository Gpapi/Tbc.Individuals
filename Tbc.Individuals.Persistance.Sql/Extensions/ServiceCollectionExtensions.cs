using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tbc.Individuals.Domain.Repositories;
using Tbc.Individuals.Persistance.Sql.Repositories;

namespace Tbc.Individuals.Persistance.Sql.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IndividualsContext>(option
            => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICitiesRepository, CitiesRepository>();
        services.AddScoped<IIndividualsRepository, IndividualsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
