using Microsoft.Extensions.DependencyInjection;
using Tbc.Individuals.Domain;

namespace Tbc.Individuals.FileStorage.Local;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileStorage(this IServiceCollection services, Action<FileStorageOptions> configure)
    {
        services.Configure(configure);
        services.AddScoped<IFileStorage, FileSystemStorage>();
        return services;
    }
}
