using Microsoft.Extensions.Options;

namespace Tbc.Individuals.Api.Localization;

public static class WebApplicationExtensions
{
    public static WebApplication UseCustomLocalization(this WebApplication app)
    {
        var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>()!.Value;
        app.UseRequestLocalization(localizationOptions);
        
        return app;
    }
}