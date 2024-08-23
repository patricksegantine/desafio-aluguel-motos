using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RentAMotto.Common.Api.Presentation;
using RentAMotto.Deliverers.Application.UseCases.RentalContracts.Create;
using RentAMotto.Deliverers.Application.UseCases.RentalContracts.Get;
using RentAMotto.Deliverers.Application.UseCases.RentalContracts.GetBalance;

namespace RentAMotto.Deliverers.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ContractsController(
    ICreateRentalContractUsecase createRentalContractUsecase,
    IGetRentalContractUsecase getRentalContractUsecase,
    IGetBalanceUsecase getBalanceUsecase) : BaseController
{
    private readonly ICreateRentalContractUsecase _createRentalContractUsecase = createRentalContractUsecase;
    private readonly IGetRentalContractUsecase _getRentalContractUsecase = getRentalContractUsecase;
    private readonly IGetBalanceUsecase _getBalanceUsecase = getBalanceUsecase;

    /// <summary>
    /// Cadastra uma nova conta de entregador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("rent")]
    [ProducesResponseType(typeof(ApiBaseResponse<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiBaseResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CreateRentalContractResult>> CreateAsync(
        [FromBody] CreateRentalContractRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _createRentalContractUsecase.Handle(request, cancellationToken));
    }

    [HttpGet("{id:int}/check-balance")]
    [ProducesResponseType(typeof(ApiBaseResponse<GetBalanceResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetBalanceResult>> GetBalanceAsync(
        [FromRoute] int id,
        [FromQuery] DateTime? returnDate = null,
        CancellationToken cancellationToken = default)
    {
        var request = new GetBalanceRequest { Id = id, ReturnDate = returnDate };
        return Ok(await _getBalanceUsecase.Handle(request, cancellationToken));
    }

    [HttpGet("{id:int}/view")]
    [ProducesResponseType(typeof(ApiBaseResponse<GetRentalContractResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetRentalContractResult>> GetAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _getRentalContractUsecase.Handle(id, cancellationToken));
    }
}
