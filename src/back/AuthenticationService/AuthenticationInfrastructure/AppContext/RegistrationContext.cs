using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationInfrastructure.AppContext
{
    public static class RegistrationContext
    {
        public static IServiceCollection AddAppContext(this IServiceCollection services)
        {
            string connection = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=kartofel12341";
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));
            return services;
        }
    }
}
