using Serilog;
using Serilog.Events;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Read Serilog configuration from appsettings
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Diagnostics", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    var dateTimeUtcNow = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

    Log.Information("Starting Web Application");

    Log.Information("UTC Time: {DateTimeUtcNow}", dateTimeUtcNow);

    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
    });

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    // Ensure fatal startup errors are captured
    Log.Fatal(ex, "Web Application start-up failed");
}
finally
{
    // Flush and close loggers
    Log.CloseAndFlush();
}
