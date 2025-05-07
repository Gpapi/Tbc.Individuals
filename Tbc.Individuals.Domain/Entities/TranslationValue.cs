namespace Tbc.Individuals.Domain.Entities;

public class TranslationValue(string language, string value)
{
    public int Id { get; }
    public string Language { get; } = language;
    public string Value { get; internal set; } = value;
}
