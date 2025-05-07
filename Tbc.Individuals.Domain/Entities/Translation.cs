using Tbc.Individuals.Domain.Resources;
using Tbc.Individuals.Domain.Utils;

namespace Tbc.Individuals.Domain.Entities;

public class Translation
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Translation()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public Translation(List<TranslationValue> translationValues)
    {
        Validate(translationValues);

        TranslationValues = translationValues;
    }

    public int Id { get; }
    public List<TranslationValue> TranslationValues { get; }

    public string GetTranslatedValue(string language, string fallbackLanguage)
    {
        return TranslationValues
                   .FirstOrDefault(x => string.Equals(x.Language, language, StringComparison.OrdinalIgnoreCase))
                   ?.Value ??
               TranslationValues.FirstOrDefault(x =>
                   string.Equals(x.Language, fallbackLanguage, StringComparison.OrdinalIgnoreCase))?.Value
               ?? throw new InvalidOperationException(Resources.Resources.SomethingWentWrong);
    }

    public string? TryGetTranslatedValue(string language, string fallbackLanguage)
    {
        return TranslationValues
                   .FirstOrDefault(x => string.Equals(x.Language, language, StringComparison.OrdinalIgnoreCase))
                   ?.Value ??
               TranslationValues.FirstOrDefault(x =>
                   string.Equals(x.Language, fallbackLanguage, StringComparison.OrdinalIgnoreCase))?.Value
               ?? null;
    }

    public string GetTranslatedValue(string language)
    {
        return TranslationValues
                   .FirstOrDefault(x => string.Equals(x.Language, language, StringComparison.OrdinalIgnoreCase))
        ?.Value
               ?? throw new InvalidOperationException(Resources.Resources.SomethingWentWrong);
    }

    public void Update(Translation translation)
    {
        if (translation == this)
        {
            return;
        }

        List<TranslationValue> translationValuesToDelete = [];

        foreach (var value in TranslationValues)
        {
            var newValue = translation.TranslationValues.FirstOrDefault(x => x.Language == value.Language);

            if (newValue != null)
            {
                value.Value = newValue.Value;
            }
            else
            {
                translationValuesToDelete.Add(value);
            }
        }

        TranslationValues.RemoveAll(x => translationValuesToDelete.Contains(x));

        foreach (var value in translation.TranslationValues)
        {
            if (TranslationValues.All(x => x.Language != value.Language))
            {
                TranslationValues.Add(value);
            }
        }
    }

    private static void Validate(List<TranslationValue> translationValues)
    {
        if (!translationValues.Any(_ => _.Language.Equals(LocalizationConfig.DefaultCulture, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException(Resources.Resources.DefaultLanguageRequired);
        }

        if (translationValues.Any(_ => !LocalizationConfig.SupportedCultures.Any(s => s.Equals(_.Language, StringComparison.OrdinalIgnoreCase))))
        {
            throw new ArgumentException(Resources.Resources.InvalidLanguage);
        }
    }
}

public static class TranslationValueExtensions
{
    public static string GetTranslatedValue(this IEnumerable<TranslationValue> values, string language,
        string fallbackLanguage)
    {
        return values.FirstOrDefault(x => string.Equals(x.Language, language, StringComparison.OrdinalIgnoreCase))
                   ?.Value ??
               values.FirstOrDefault(x =>
                   string.Equals(x.Language, fallbackLanguage, StringComparison.OrdinalIgnoreCase))?.Value
               ?? throw new InvalidOperationException(Resources.Resources.SomethingWentWrong);
    }
}
