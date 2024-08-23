using ErrorOr;
using Microsoft.AspNetCore.Http;
using RentAMotto.Common.Abstraction.Services;
using RentAMotto.Domain;
using RentAMotto.Domain.Repositories;

namespace RentAMotto.Deliverers.Application.UseCases.Deliverers.DrivingLicence;

public sealed class UploadDrivingLicenceUsecase(
    IDeliveryDriverRepository deliveryDriverRepository,
    IStorageService storageService) : IUploadDrivingLicenceUsecase
{
    private readonly IDeliveryDriverRepository _deliveryDriverRepository = deliveryDriverRepository;
    private readonly IStorageService _storageService = storageService;

    public async Task<ErrorOr<Success>> Handle(int id, IFormFile file, CancellationToken cancellationToken = default)
    {
        if (file == null || (file.ContentType != "image/png" && file.ContentType != "image/bmp"))
            return ErrorCatalog.InvalidDrivingLicenceFormat;

        var deliveryDriver = await _deliveryDriverRepository.GetByIdAsync(id, cancellationToken);
        if (deliveryDriver is null)
            return ErrorCatalog.DeliveryDriverNotFound;

        var drivingLicenceNumber = deliveryDriver.DrivingLicenceNumber;
        var fileName = $"{drivingLicenceNumber}{Path.GetExtension(file.FileName)}";

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream, cancellationToken);

        var (success, url) = await _storageService.SaveFileAsync(fileName, stream.ToArray());
        if (!success)
            return ErrorCatalog.ErrorWhileSavingDrivingLicence;

        deliveryDriver.UpdateDriverLicenseImage(url!);

        return Result.Success;
    }
}
