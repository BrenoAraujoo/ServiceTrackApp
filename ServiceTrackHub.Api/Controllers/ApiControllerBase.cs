using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Domain.Enums.Common.Result;

namespace ServiceTrackHub.Api.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult ApiControllerHandleResult(Result result)
        {

            return result.Error.Code switch
            {
                1 => NotFound(result),
                2 => BadRequest(result),
                3 => Conflict(result),
                4 => Unauthorized(result),
                _ => StatusCode(500, "Internal Server Error")

            };
        }

    }
}
