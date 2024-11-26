using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using System;

namespace CommonShared.RegistrationServices
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            var assemblies = Directory
                .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(file => Path.GetFileNameWithoutExtension(file)
                .Contains("Application"))
                .Select(Assembly.LoadFrom)
                .ToArray();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            return services;
        }
    }
}
