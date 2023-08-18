using System.Reflection;
using BankCrud.Service.Concrete;
using BankCrud.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankCrud.Service;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services
         )
    {
       
        ResolveAllTypes(services, ServiceLifetime.Scoped, typeof(IBaseService<,>), "Service");
        services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
        return services;
    }

    public static void ResolveAllTypes(IServiceCollection services, ServiceLifetime serviceLifetime, Type refType,
        string suffix)
    {

        var assemblyCurrent = refType.GetTypeInfo().Assembly;
        var allServices = assemblyCurrent.GetTypes().Where(t =>
            t.GetTypeInfo().IsClass &&
            !t.GetTypeInfo().IsAbstract &&
            !t.GetType().IsInterface &&
            t.Name.EndsWith(suffix)
        );


        foreach (var type in allServices)
        {
            var allInterfaces = type.GetInterfaces();
            var mainInterfaces = allInterfaces.Except
                (allInterfaces.SelectMany(t => t.GetInterfaces()));
            foreach (var itype in mainInterfaces)
            {
                if (allServices.Any(x => !x.Equals(type) && itype.IsAssignableFrom(x)))
                {
                    throw new Exception("The " + itype.Name +
                                        " type has more than one implementations, please change your filter");
                }

                services.Add(new ServiceDescriptor(itype, type, serviceLifetime));
            }
        }
    }

}