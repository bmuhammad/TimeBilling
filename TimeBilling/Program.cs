

using FluentValidation;
using FreeBilling.Data.Entities;
using Mapster;
using System.Reflection;
using TimeBilling.Apis;
using TimeBilling.Data;
using TimeBilling.Services;
using TimeBilling.Validators;

var builder = WebApplication.CreateBuilder(args);

IConfigurationBuilder configBuilder = builder.Configuration;
configBuilder.Sources.Clear();
configBuilder.AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.development.json", true)
    .AddUserSecrets(Assembly.GetExecutingAssembly())
    .AddEnvironmentVariables()
    .AddCommandLine(args);

builder.Services.AddDbContext<BillingContext>();
builder.Services.AddScoped<IBillingRepository, BillingRepository>();

//Adds needed dependencies
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailService, DevTimeEmailService>();

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<TimeBillModelValidator>();

TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetEntryAssembly()!);

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    //Sets up error page for development
    app.UseDeveloperExceptionPage();
}

//Allows us to server index.html as the default webpage
app.UseDefaultFiles();

//Allows us to serve files from wwwroot
app.UseStaticFiles();

//Directs to look under folder structure for razor files
app.MapRazorPages();

TimeBillsApi.Register(app);

app.MapControllers();

app.Run();

