using Newtonsoft.Json;

namespace ServiceTrackApp.Api.Middleware;

public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomAuthorizationMiddleware> _logger;

    public CustomAuthorizationMiddleware(RequestDelegate next, ILogger<CustomAuthorizationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == 401)
        {
            context.Response.ContentType = "application/json";
            _logger.LogError("401 Unauthorized");
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                { error = "Acesso negado. Você não tem permissão para acessar este recurso." }));
        }
    }
}