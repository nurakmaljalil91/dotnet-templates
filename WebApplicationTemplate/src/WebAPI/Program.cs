using Application;
using Infrastructure;
using System.Globalization;
using Serilog;
using Serilog.Events;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Read Serilog configuration from appsettings
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    var dateTimeUtcNow = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

    Log.Information("Starting Web Application");

    Log.Information("UTC Time: {DateTimeUtcNow}", dateTimeUtcNow);

    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddWebAPIServices(builder.Configuration);

    builder.Services.AddControllers();
   
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        // Add OpenAPI 3.0 document serving middleware
        app.UseOpenApi();

        // Add web UIs to interact with the document
        app.UseSwaggerUi();

        // Add ReDoc UI to interact with the document
        app.UseReDoc(options => { options.Path = "/redoc"; });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
    });

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex)
{
    // Ensure fatal startup errors are captured
    Log.Fatal(ex, "Web Application start-up failed");
}
finally
{
    // Flush and close loggers
    await Log.CloseAndFlushAsync();
}
