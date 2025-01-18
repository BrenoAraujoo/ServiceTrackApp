using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Common.Result;

namespace ServiceTrackApp.Api.Middleware;

public class CustomAuthorizationMiddleware(RequestDelegate next, ILogger<CustomAuthorizationMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        //Serializes as camel case
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        if (context.Response.StatusCode == 401)
        {
            context.Response.ContentType = "application/json";
            logger.LogError("401 Unauthorized");
            var resp = JsonConvert.SerializeObject(
                Result.Failure(CustomError.AuthenticationError(ErrorMessage.NotAuthenticated)), settings);
            await context.Response.WriteAsync(resp);
        }

        if (context.Response.StatusCode == 403)
        {
            context.Response.ContentType = "application/json";
            logger.LogError("403 Forbidden");
            var resp = JsonConvert.SerializeObject
                (Result.Failure(CustomError.AuthorizationError(ErrorMessage.Unauthorized)), settings);
            await context.Response.WriteAsync(resp);
        }
    }
}