namespace Tbc.Individuals.Domain;

public interface IFileStorage
{
    Task<string> Upload(Stream fileStream, string fileName, CancellationToken cancellationToken = default);
    Task<bool> Delete(string fileName, CancellationToken cancellationToken = default);
}
