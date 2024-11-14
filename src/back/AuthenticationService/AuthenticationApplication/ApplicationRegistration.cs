using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthenticationApplication
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
