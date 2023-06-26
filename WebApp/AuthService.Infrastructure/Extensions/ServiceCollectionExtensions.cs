using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthService.Domain;
using AuthService.Infrastructure.Contexts;
using AuthService.Infrastructure.Managers;

namespace AuthService.Infrastructure.Extensions
{

    public static class ServiceCollectionExtensions
    {
        // Добавляем бизнес-логику
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddManagers();
            services.AddDatabase(configuration);
            return services;
        }
        //Добавление менеджеров
        private static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IAuthManager, AuthManager>();
            return services;
        }

        //Добавление Базы Данных
        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserContext>(builder =>
                builder.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}