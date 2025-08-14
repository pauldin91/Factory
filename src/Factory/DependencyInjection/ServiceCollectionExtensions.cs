using System.ComponentModel;
using Factory.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Factory.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFactory<TIfc,TImpl>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            if(!typeof(TIfc).IsInterface)
                throw new ArgumentException(string.Format($"Type {typeof(TIfc).FullName} must be an interface."));
            
            if(!typeof(TImpl).IsAssignableTo(typeof(TIfc)))
                throw new ArgumentException(string.Format($"Type {typeof(TImpl).FullName} must be implement Interface {typeof(TIfc).FullName}."));
            
            var implementors = typeof(TImpl)
                .Assembly
                .GetTypes()
                .Where(s => s.IsAssignableTo(typeof(TIfc)) && !s.IsInterface && !s.IsAbstract);

            foreach (var implementor in implementors)
            {
                services.Add(new ServiceDescriptor(typeof(TIfc), implementor, lifetime));
            }

            services.Add(new ServiceDescriptor(typeof(IFactory<>),typeof(FactoryWrapperImpl<>),lifetime));

            return services;
        }
    }
}