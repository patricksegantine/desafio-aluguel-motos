using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Create;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Delete;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Get;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Search;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Update;
using RentAMotto.Common.Abstraction.Pagination;
using RentAMotto.Common.Api.Presentation;
using RentAMotto.Domain.Queries;

namespace RentAMotto.Admin.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MotorcyclesController(
    ICreateMotorcycleUsecase createMotorcycleUsecase, 
    ISearchMotorcycleUsecase searchMotorcycleUsecase, 
    IGetMotorcycleUsecase getMotorcycleUsecase, 
    IUpdateMotorcycleUsecase updateMotorcycleUsecase, 
    IDeleteMotorcycleUsecase deleteMotorcycleUsecase) : BaseController
{
    private readonly ICreateMotorcycleUsecase _createMotorcycleUsecase = createMotorcycleUsecase;
    private readonly ISearchMotorcycleUsecase _searchMotorcycleUsecase = searchMotorcycleUsecase;
    private readonly IGetMotorcycleUsecase _getMotorcycleUsecase = getMotorcycleUsecase;
    private readonly IUpdateMotorcycleUsecase _updateMotorcycleUsecase = updateMotorcycleUsecase;
    private readonly IDeleteMotorcycleUsecase _deleteMotorcycleUsecase = deleteMotorcycleUsecase;

    /// <summary>
    /// Cadastra uma nova moto no sistema
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiBaseResponse<CreateMotorcycleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiBaseResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CreateMotorcycleResult>> CreateAsync(
        [FromBody] CreateMotorcycleRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _createMotorcycleUsecase.Handle(request, cancellationToken));
    }

    /// <summary>
    /// Listagem de motos cadastradas. Filtra por placa
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("search")]
    [ProducesResponseType(typeof(ApiBaseResponse<PagedResult<VehicleSummary>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<VehicleSummary>>> SearchAsync(
        [FromQuery] SearchMotorcycleRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _searchMotorcycleUsecase.Handle(request, cancellationToken));
    }

    /// <summary>
    /// Obtem os dados de uma moto
    /// </summary>
    /// <param name="id">Código da moto</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiBaseResponse<GetMotorcycleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetMotorcycleResult>> GetAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _getMotorcycleUsecase.Handle(id, cancellationToken));
    }

    /// <summary>
    /// Atualiza a placa de uma moto
    /// </summary>
    /// <param name="id">Código da moto</param>
    /// <param name="updateRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiBaseResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> UpdateAsync(
        [FromRoute] int id,
        [FromBody] UpdateMotorcycleRequest updateRequest,
        CancellationToken cancellationToken = default)
    {
        var request = updateRequest with { Id = id };
        return NoContent(await _updateMotorcycleUsecase.Handle(request, cancellationToken));
    }

    /// <summary>
    /// Exclui uma moto
    /// </summary>
    /// <param name="id">Código da moto</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiBaseResponse<object>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> DeleteAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken = default)
    {
        return NoContent(await _deleteMotorcycleUsecase.Handle(id, cancellationToken));
    }
}
