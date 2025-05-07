using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Tbc.Individuals.Application.Helpers;
using Tbc.Individuals.Domain.Utils;

namespace Tbc.Individuals.Api.Localization;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomLocalization(this IServiceCollection services, IConfiguration configuration)
    {
        var cultures = configuration.GetSection("SupportedCultures").Get<string[]>();

        if (cultures == null || cultures.Length == 0)
        {
            throw new ArgumentException("Missing supported cultures.", nameof(cultures));
        }
        
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = cultures.Select(CultureInfo.GetCultureInfo).ToArray();

            options.DefaultRequestCulture = new RequestCulture(cultures.First()); // Default language
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            
            options.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());
        });

        services.AddScoped<LocalizationHelper>();
        LocalizationConfig.Configure(cultures.First(), cultures);

        return services;
    }
}