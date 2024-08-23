using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RentAMotto.Common.Api.Presentation;

namespace RentAMotto.Deliverers.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PlansController : BaseController
{

}