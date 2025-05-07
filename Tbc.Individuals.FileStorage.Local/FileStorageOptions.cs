namespace Tbc.Individuals.FileStorage.Local;

public class FileStorageOptions
{
    public const string Name = "FileStorage";
    public string Folder { get; set; } = "uploads";
    public long MaxFileSizeInBytes { get; set; } = 10 * 1024 * 1024; // 10 MB default
    public string[] AllowedExtensions { get; set; } = [".jpg", ".jpeg", ".png", ".webp"];
}
