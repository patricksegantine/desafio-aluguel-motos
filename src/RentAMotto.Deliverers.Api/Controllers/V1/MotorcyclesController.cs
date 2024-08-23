using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RentAMotto.Common.Abstraction.Pagination;
using RentAMotto.Common.Api.Presentation;
using RentAMotto.Deliverers.Application.UseCases.Motorcycles.Search;
using RentAMotto.Domain.Queries;

namespace RentAMotto.Deliverers.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class MotorcyclesController(ISearchMotorcycleUsecase searchMotorcycleUsecase) : BaseController
{
    private readonly ISearchMotorcycleUsecase _searchMotorcycleUsecase = searchMotorcycleUsecase;

    /// <summary>
    /// Listagem de motos
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
}
