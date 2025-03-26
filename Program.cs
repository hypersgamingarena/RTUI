using Microsoft.EntityFrameworkCore;
using RTUI.Data;
using RTUI.Models;
using Microsoft.Extensions.Logging;
using RTUI.Services;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Add services to the container.
    builder.Services.AddControllersWithViews()
        .AddRazorRuntimeCompilation();

    // Provide the project root directory path (could be adjusted if needed)
    var projectRoot = Directory.GetCurrentDirectory();
    builder.Services.AddTransient<AIDebuggingService>(provider => new AIDebuggingService(projectRoot));

    // Configure DbContext with connection string
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Add logging
    builder.Services.AddLogging(logging => logging.AddConsole().AddDebug());
}
catch (Exception ex)
{
    // Log the exception during startup
    var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();
    logger.LogError(ex, "An error occurred during service configuration.");
    throw;
}

var app = builder.Build();

// Apply migrations and seed the database if it's empty
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // Apply migrations to ensure the database is up-to-date
    context.Database.Migrate();

    // Seed the database
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Default route configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
