using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceTrackHub.Application.Services;
using ServiceTrackHub.Domain.Interfaces;
using ServiceTrackHub.Infra.Data.Context;
using ServiceTrackHub.Infra.Data.Repositories;
using ServiceTrackHub.Application.Interfaces;
using ServiceTrackHub.Application.Mappings;

namespace ServiceTrackHub.Infra.IoC
{
    public  static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
                (options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            
            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
          
            services.AddScoped<ITasksService, TasksService>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddAutoMapper(typeof(AutomapConfig));
            return services;
        }
    }
}
