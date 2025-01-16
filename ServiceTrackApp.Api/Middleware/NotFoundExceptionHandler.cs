using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackApp.Application.Exceptions;

namespace ServiceTrackApp.Api.Middleware
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception,
            CancellationToken cancellationToken)
        {
            
            //If the exception can't be handled, return false.
            if(exception is not NotFoundException)
            {
                return false;
            }
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not found"
            };
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
