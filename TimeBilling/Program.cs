

using System.Reflection;
using TimeBilling.Data;
using TimeBilling.Services;

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

app.MapControllers();

app.Run();

