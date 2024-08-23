namespace RentAMotto.Common.Abstraction.Services;

public interface IStorageService
{
    Task<(bool success, string? url)> SaveFileAsync(string fileName, byte[] content);
}
