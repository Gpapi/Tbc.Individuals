namespace Tbc.Individuals.Domain.Utils;

public static class LocalizationConfig
{
    private static string _defaultCulture = "ka";
    private static string[] _supportedCultures = ["ka", "en"];

    public static void Configure(string defaultCulture, string[] supportedCultures)
    {
        _defaultCulture = defaultCulture;
        _supportedCultures = supportedCultures;
    }

    public static string DefaultCulture => _defaultCulture;
    public static string[] SupportedCultures => _supportedCultures;
}
