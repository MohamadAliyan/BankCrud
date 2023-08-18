using System.Reflection;
using BankCrud.Interceptors;
using BankCrud.Repository.Concrate;
using BankCrud.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankCrud.Repository;

public static class ConfigureServices

{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        //services.Configure<AppSettings>(configuration);
        ResolveAllTypes(services, ServiceLifetime.Scoped, typeof(Repository<>), "Repository");
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IApplicationContext, ApplicationDbContext> ();

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