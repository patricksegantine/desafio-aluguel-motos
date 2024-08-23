using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RentAMotto.Common.Abstraction.Pagination;
using RentAMotto.Common.Api.Presentation;
using RentAMotto.Deliverers.Application.UseCases.RentalPlans.Search;
using RentAMotto.Domain.Queries;

namespace RentAMotto.Deliverers.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PlansController(ISearchRentalPlanUsecase searchRentalPlanUsecase) : BaseController
{
    private readonly ISearchRentalPlanUsecase _searchRentalPlanUsecase = searchRentalPlanUsecase;

    /// <summary>
    /// Listagem de planos. Filtra por descrição
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("search")]
    [ProducesResponseType(typeof(ApiBaseResponse<PagedResult<RentalPlanSummary>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<RentalPlanSummary>>> SearchAsync(
        [FromQuery] SearchRentalPlanRequest request,
        CancellationToken cancellationToken = default)
    {
        return Ok(await _searchRentalPlanUsecase.Handle(request, cancellationToken));
    }
}
