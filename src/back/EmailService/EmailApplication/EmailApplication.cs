using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EmailApplication
{
    public static class EmailApplication
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
