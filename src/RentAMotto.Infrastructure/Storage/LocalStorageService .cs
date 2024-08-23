using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RentAMotto.Common.Abstraction.Services;

namespace RentAMotto.Infrastructure.Storage;

public class LocalStorageService : IStorageService
{
    private readonly string _storagePath;
    private readonly ILogger<LocalStorageService> _logger;

    public LocalStorageService(ILogger<LocalStorageService> logger, IConfiguration configuration)
    {
        _logger = logger;

        var relativePath = configuration["StoragePath"];
        _storagePath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }

    public async Task<(bool success, string? url)> SaveFileAsync(string fileName, byte[] content)
    {
        try
        {
            var filePath = Path.Combine(_storagePath, fileName);
            await File.WriteAllBytesAsync(filePath, content);
            return (true, filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while saving the file {FileName}", fileName);
            return (false, null);
        }
    }
}
