using TimeBilling.Services;

var builder = WebApplication.CreateBuilder(args);

//Adds needed dependencies
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailService, DevTimeEmailService>();

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

app.Run();

