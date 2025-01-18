using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceTrackApp.Application.Interfaces;
using ServiceTrackApp.Application.Interfaces.Auth;
using ServiceTrackApp.Application.Interfaces.Domain;
using ServiceTrackApp.Application.Services;
using ServiceTrackApp.Application.Services.Auth;
using ServiceTrackApp.Application.Services.Domain;
using ServiceTrackApp.Domain.Interfaces;
using ServiceTrackApp.Infra.Data.Context;
using ServiceTrackApp.Infra.Data.Repositories;

namespace ServiceTrackApp.Infra.IoC
{
    public  static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), 
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            
            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskTypeRepository, TaskTypeRepository>();
            
          
            services.AddScoped<ITasksService, TasksService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskTypeService, TaskTypeService>();
            

            //Auth
            services.AddScoped<IHashService, HashService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAuthService, AuthService>();
            
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContextService, UserContextService>();

            return services;
        }
    }
}
