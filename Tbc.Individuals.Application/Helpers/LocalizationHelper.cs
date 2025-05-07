using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System.Globalization;
using Tbc.Individuals.Domain.Entities;

namespace Tbc.Individuals.Application.Helpers;

public class LocalizationHelper(IOptions<RequestLocalizationOptions> options)
{
    public string CurrentCulture => CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToLower();
    public string DefaultCulture => options.Value.DefaultRequestCulture.Culture.TwoLetterISOLanguageName.ToLower();
    public string[] SupportedCultures => [.. options.Value.SupportedCultures!.Select(culture => culture.TwoLetterISOLanguageName)];

    public string? GetTranslatedValue(Translation? translation)
    {
        if (translation == null)
        {
            return null;
        }

        return translation.GetTranslatedValue(CurrentCulture, DefaultCulture);
    }

    public string? TryGetTranslatedValue(Translation? translation)
    {
        if (translation == null)
        {
            return "";
        }

        return translation.TryGetTranslatedValue(CurrentCulture, DefaultCulture);
    }

    public string? GetDefaultTranslatedValue(Translation? translation)
    {
        if (translation == null)
        {
            return null;
        }

        return translation.GetTranslatedValue(DefaultCulture);
    }
}
