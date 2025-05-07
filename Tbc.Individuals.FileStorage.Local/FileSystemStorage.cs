using Microsoft.Extensions.Options;
using Tbc.Individuals.Domain;

namespace Tbc.Individuals.FileStorage.Local;

public class FileSystemStorage : IFileStorage
{
    private readonly string _uploadsRoot;
    private readonly string _requestPath;
    private readonly FileStorageOptions _options;

    public FileSystemStorage(IOptions<FileStorageOptions> options)
    {
        _options = options.Value;

        if (string.IsNullOrWhiteSpace(_options.Folder))
            throw new InvalidOperationException("Storage folder path is not configured.");

        _uploadsRoot = _options.Folder;
        _requestPath = "/" + Path.GetFileName(_uploadsRoot).Trim('/');

        Directory.CreateDirectory(_uploadsRoot);
    }

    public async Task<string> Upload(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        if (fileStream == null || fileStream.Length == 0)
            throw new ArgumentException("File stream is empty.", nameof(fileStream));

        if (fileStream.Length > _options.MaxFileSizeInBytes)
            throw new InvalidOperationException("File is too large.");

        fileName = Path.GetFileName(fileName);
        var extension = Path.GetExtension(fileName).ToLowerInvariant();

        if (!_options.AllowedExtensions.Contains(extension))
            throw new InvalidOperationException("File type is not allowed.");

        var randomName = $"{Guid.NewGuid():N}{extension}";
        var fullPath = Path.Combine(_uploadsRoot, randomName);

        await using var output = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
        await fileStream.CopyToAsync(output, cancellationToken);

        return $"{_requestPath}/{randomName}";
    }


    public Task<bool> Delete(string fileName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return Task.FromResult(false);

        fileName = Path.GetFileName(fileName);
        var fullPath = Path.Combine(_uploadsRoot, fileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}


