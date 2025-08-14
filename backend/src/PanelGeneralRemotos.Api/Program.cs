using PanelGeneralRemotos.Application.Services.Interfaces;
using PanelGeneralRemotos.Application.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ CORREGIDO: Registro único de servicios (eliminé duplicados)
builder.Services.AddScoped<IGoogleSheetsService, GoogleSheetsService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ICallRecordService, CallRecordService>();
builder.Services.AddScoped<IPerformanceMetricService, PerformanceMetricService>();

// ✅ AGREGADO: SignalR para tiempo real
builder.Services.AddSignalR();

// ✅ CORREGIDO: CORS más completo
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Importante para SignalR
    });
});

// ✅ AGREGADO: Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ CORREGIDO: Orden correcto de middleware
app.UseCors("AllowFrontend");
app.UseAuthorization();

// ✅ AGREGADO: Health check endpoint
app.MapHealthChecks("/health");

app.MapControllers();

// ✅ AGREGADO: SignalR Hub (cuando lo implementes)
// app.MapHub<DashboardHub>("/dashboardHub");

app.Run();