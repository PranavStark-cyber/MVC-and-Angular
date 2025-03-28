using MVCTest.Service;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Add HttpClient for DvdService
builder.Services.AddHttpClient<DvdService>();

// Add controllers with views (for MVC)
builder.Services.AddControllersWithViews();

// CORS configuration to allow Angular app to call the API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        // You can specify a specific origin for security purposes (e.g., http://localhost:4200)
        policy.WithOrigins("http://localhost:4200") // Angular's default port
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register DvdService as a singleton service
builder.Services.AddSingleton<DvdService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use CORS middleware to allow cross-origin requests from Angular
app.UseCors();

app.UseAuthorization();

app.MapControllers();  // Map controller routes (API)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
