using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ServiceTrackApp.Api.ExceptionsHandlers;
using ServiceTrackApp.Api.Middleware;
using ServiceTrackApp.Infra.IoC;


var builder = WebApplication.CreateBuilder(args);

ConfigureServices();
ConfigureSwagger();
builder.Services.AddInfrastructure(builder.Configuration);
ConfigureExceptionHandler();
builder.Services.AddProblemDetails();
ConfigureAuthentication();
ConfigureAuthorization();
ConfigureCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
ConfigureCustomMiddlewares();
app.UseExceptionHandler();
app.UseCors("AllowSpecificOrigin");


// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.Run();
return;

void ConfigureCustomMiddlewares()
{
    app.UseMiddleware<CustomAuthorizationMiddleware>();
}

void ConfigureSwagger()
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Bearer {seu token}"
        });
    
    
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<String>()
            }
        });
    
    });

}

void ConfigureAuthorization()
{
    builder.Services.AddAuthorization();
}

void ConfigureAuthentication()
{
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}

void ConfigureCors()
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            policyBuilder => policyBuilder.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader());
    });
}

/// <summary>
/// Add multiple IExceptionHandler implementations, and they're called in the order they are registered.
/// The last registered must be GlobalExceptionHandler to handle generic exceptions (Internal Server Error).
/// </summary>
/// <returns></returns>
void ConfigureExceptionHandler()
{
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
}

void ConfigureServices()
{
    builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });
}