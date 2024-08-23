using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace RentAMotto.Deliverers.Application.UseCases.Deliverers.DrivingLicence;

public interface IUploadDrivingLicenceUsecase
{
    Task<ErrorOr<Success>> Handle(int id, IFormFile file, CancellationToken cancellationToken = default);
}