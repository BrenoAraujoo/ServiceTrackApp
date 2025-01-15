using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackApp.Application.Exceptions;

namespace ServiceTrackApp.Api.Middleware
{
    public class BadRequestExceptionHandler : IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception,
            CancellationToken cancellationToken)
        {

            ProblemDetails? problemDetails;
            if(exception is not BadRequestException)
            {
                return false;
            }
            problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad request!!"
            };
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
