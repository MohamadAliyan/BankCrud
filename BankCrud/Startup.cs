using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BankCrud.Repository.Interfaces;
using BankCrud.Repository;
using BankCrud.Repository.Concrate;
using BankCrud.Service;
using Microsoft.OpenApi.Models;

namespace BankCrud.Api;
public class Startup
{
    public IConfiguration configRoot
    {
        get;
    }
    
    public Startup(IConfiguration configuration)
    {
        configRoot = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddInfrastructureServices(configRoot);
        services.AddServices();
     
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        //services.AddSwaggerGen();


        services.AddSwaggerGen(swagger =>
        {
            //This is to generate the Default UI of Swagger Documentation
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = " Bank api",
                Description = "ASP.NET Core 7.0 Web API"
            });
            // To Enable authorization using Swagger (JWT)
           

        });
        services.AddControllers();
        

    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {

        app.UseSwagger();
        
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
        });


        app.UseRouting();
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseAuthentication();

        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {

            endpoints.MapControllers();
            //endpoints.MapControllerRoute("default", "api/{controller=Home}/{action=Index}/{id?}");

        });

        //app.MapControllers();
        app.Run();
    }
}










