using Factory.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Factory.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFactoryFromAssembly(this IServiceCollection services, Assembly assembly,ServiceLifetime lifetime=ServiceLifetime.Scoped)
        {
            var implementors = assembly
                .GetTypes()
                .Where(s => s.IsAssignableTo(typeof(IImplementor)) && !s.IsInterface && !s.IsAbstract);

            services.AddSingleton(typeof(IFactory), typeof(FactoryWrapperImpl));
            
            foreach (var implementor in implementors)
            {
                services.Add(new ServiceDescriptor(implementor.GetType(), Activator.CreateInstance(implementor),lifetime));
            }

            return services;
        }
    }
}