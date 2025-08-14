using PanelGeneralRemotos.Application.Services.Interfaces;
using PanelGeneralRemotos.Application.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Panel General Remotos API", 
        Version = "v1",
        Description = "API para el dashboard de seguimiento de llamadas remotas"
    });
});

// ✅ Registro de servicios de aplicación
builder.Services.AddScoped<IGoogleSheetsService, GoogleSheetsService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ICallRecordService, CallRecordService>();
builder.Services.AddScoped<IPerformanceMetricService, PerformanceMetricService>();

// ✅ SignalR para tiempo real
builder.Services.AddSignalR();

// ✅ CORS configurado correctamente
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200", 
                "https://localhost:4200",
                "http://localhost:3000",
                "https://panel-remotos.vercel.app" // Para producción
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Importante para SignalR
    });
});

// ✅ Health Checks
builder.Services.AddHealthChecks()
    .AddCheck("self", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy())
    .AddCheck("google_sheets", () => Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy()); // TODO: Implementar check real

// ✅ Configuración de logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// ✅ Response compression para mejor performance
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Panel General Remotos API v1");
        c.RoutePrefix = "swagger";
    });
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// ✅ Middleware en orden correcto
app.UseResponseCompression();
app.UseHttpsRedirection();

// ✅ CORS debe ir antes de Authorization
app.UseCors("AllowFrontend");

app.UseRouting();
app.UseAuthorization();

// ✅ Health check endpoint
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var response = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(x => new
            {
                name = x.Key,
                status = x.Value.Status.ToString(),
                exception = x.Value.Exception?.Message,
                duration = x.Value.Duration.ToString()
            }),
            timestamp = DateTime.UtcNow
        };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
});

// ✅ API Controllers
app.MapControllers();

// ✅ SignalR Hub (cuando se implemente)
// app.MapHub<DashboardHub>("/api/dashboardHub");

// ✅ Endpoint de información básica
app.MapGet("/", () => new
{
    service = "Panel General Remotos API",
    version = "1.0.0",
    status = "Running",
    timestamp = DateTime.UtcNow,
    endpoints = new[]
    {
        "/health - Health check",
        "/swagger - API Documentation", 
        "/api/test - Test endpoints"
    }
}).WithName("Root").WithOpenApi();

// ✅ Logging de inicio
app.Logger.LogInformation("🚀 Panel General Remotos API iniciándose...");
app.Logger.LogInformation("📊 Dashboard API disponible en: {BaseUrl}", 
    app.Environment.IsDevelopment() ? "http://localhost:5000" : "https://panel-remotos-api.railway.app");

app.Run();