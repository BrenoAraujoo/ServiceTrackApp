using Microsoft.AspNetCore.Mvc;
using ServiceTrackHub.Domain.Common.Result;

namespace ServiceTrackHub.Api.Controllers
{
    public class ApiControllerBase :ControllerBase
    {
        protected IActionResult ApiControllerHandleResult(Result result)
        {

            return result.Error.Code switch
            {
                "RecordNotFound" => NotFound(result),
                _ => StatusCode(500, "Internal Server Error")

            };
        }

    }
}
