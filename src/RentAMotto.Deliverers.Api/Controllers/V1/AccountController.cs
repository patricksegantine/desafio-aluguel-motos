using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RentAMotto.Common.Api.Presentation;
using RentAMotto.Deliverers.Application.UseCases.Deliverers.DrivingLicence;
using RentAMotto.Deliverers.Application.UseCases.Deliverers.Register;

namespace RentAMotto.Deliverers.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AccountController(
    IRegisterDeliveryDriverUsecase registerDeliveryDriverUsecase,
    IUploadDrivingLicenceUsecase uploadDrivingLicenceUsecase) : BaseController
{
    private readonly IRegisterDeliveryDriverUsecase _registerDeliveryDriverUsecase = registerDeliveryDriverUsecase;
    private readonly IUploadDrivingLicenceUsecase _uploadDrivingLicenceUsecase = uploadDrivingLicenceUsecase;

    /// <summary>
    /// Cadastra uma nova conta de entregador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiBaseResponse<RegisterDeliveryDriverResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiBaseResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<RegisterDeliveryDriverResult>> CreateAsync(
        [FromBody] RegisterDeliveryDriverRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _registerDeliveryDriverUsecase.Handle(request, cancellationToken));
    }

    /// <summary>
    /// Cadastra uma nova conta de entregador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/upload-driving-licence")]
    [ProducesResponseType(typeof(ApiBaseResponse<RegisterDeliveryDriverResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiBaseResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UploadDrivingLicenceAsync(
        [FromRoute] int id,
        IFormFile file,
        CancellationToken cancellationToken = default)
    {
        return NoContent(await _uploadDrivingLicenceUsecase.Handle(id, file, cancellationToken));
    }
}
