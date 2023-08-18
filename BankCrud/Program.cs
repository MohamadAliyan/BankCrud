using BankCrud.Api;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services); // calling ConfigureServices method

var app = builder.Build();
startup.Configure(app, builder.Environment); // calling Configure method

public partial class Program { }
