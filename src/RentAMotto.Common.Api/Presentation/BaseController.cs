using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace RentAMotto.Common.Api.Presentation;

[ExcludeFromCodeCoverage]
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
[Produces("application/json", [])]
[Consumes("application/json", [])]
public class BaseController : ControllerBase
{
    [NonAction]
    protected ActionResult Created(object? value)
    {
        return Present(value, HttpStatusCode.Created);
    }

    [NonAction]
    protected ActionResult<TValue> Created<TValue>(ErrorOr<TValue> result)
    {
        return Present(result, HttpStatusCode.Created);
    }

    [NonAction]
    protected new ActionResult Ok(object? value)
    {
        return Present(value, HttpStatusCode.OK);
    }

    [NonAction]
    protected ActionResult<TValue> Ok<TValue>(ErrorOr<TValue> result)
    {
        return Present(result, HttpStatusCode.OK);
    }

    [NonAction]
    protected ActionResult NoContent(object? result)
    {
        return Present(result, HttpStatusCode.NoContent);
    }

    [NonAction]
    protected ActionResult Error(IErrorOr error)
    {
        return Present(error);
    }

    private ActionResult Present(object? value, HttpStatusCode statusCode)
    {
        if (value is null)
        {
            return new StatusCodeResult((int)statusCode);
        }

        if (value is IErrorOr error && error.IsError)
        {
            return Present(error);
        }

        if (statusCode == HttpStatusCode.NoContent)
        {
            return new NoContentResult();
        }

        var response = new ApiBaseResponse<object>
        {
            Success = true,
            Data = value
        };

        return new ObjectResult(response) { StatusCode = (int)statusCode };
    }

    private ActionResult Present(IErrorOr error)
    {
        if (error is null)
        {
            ArgumentNullException.ThrowIfNull(nameof(error));
        }

        if (error!.Errors!.Exists(e => e.Type == ErrorType.NotFound))
        {
            return NotFound();
        }

        var result = new ApiBaseResponse<object>
        {
            Success = false,
            Data = null,
            Errors = error.Errors
        };

        return new ObjectResult(result) { StatusCode = StatusCodes.Status422UnprocessableEntity };
    }

    private ActionResult<TValue> Present<TValue>(ErrorOr<TValue> result, HttpStatusCode statusCode)
    {
        if (result.IsError)
        {
            return Present(result);
        }

        return Present(result.Value, statusCode);
    }
}

