using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using Serilog;
using Subasta.Aplication.Profiles;
using Subasta.Aplication.Services.Implementations;
using Subasta.Aplication.Services.Interfaces;
using Subasta.Infraestructure.Data;
using Subasta.Infraestructure.Repository.Interfaces;
using System.Text;
using Subasta.Web.Middleware;
using System.Runtime.Intrinsics.X86;
using Subasta.Infraestructure.Repository.Implementations;

//LOGGER CON SERILOG
// ======================= 
// Configurar Serilog 
// ======================= 
// Crear carpeta Logs automáticamente (evita errores si no existe) 
Directory.CreateDirectory("Logs");

// Configuración Serilog 
var logger = new LoggerConfiguration()
    // Nivel mínimo global (recomendado: Information) 
    .MinimumLevel.Information()

    // Reducir ruido de logs internos de Microsoft 
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    //Mostrar SQL ejecutado por EF Core 
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command",
LogEventLevel.Information)

    // Enriquecer logs con contexto (RequestId, etc.) 
    .Enrich.FromLogContext()

    // Consola: útil para depurar en Visual Studio 
    .WriteTo.Console()

    // Archivos separados por nivel (rolling diario) 
    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
        .WriteTo.File(@"Logs \Info-.log",
            shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))

    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
        .WriteTo.File(@"Logs \Warning-.log",
            shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))

    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
        .WriteTo.File(@"Logs \Error-.log",
        shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))

    .WriteTo.Logger(l => l
        .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
        .WriteTo.File(@"Logs \Fatal-.log",
            shared: true,
            encoding: Encoding.UTF8,
            rollingInterval: RollingInterval.Day))

    .CreateLogger();

// Paso obligatorio ANTES de crear builder 
Log.Logger = logger;

var builder = WebApplication.CreateBuilder(args);


// Integrar Serilog al host 
builder.Host.UseSerilog(Log.Logger);

// Add services to the container.
builder.Services.AddControllersWithViews();

//*********** 
// ======================= 
// Configurar Dependency Injection 
// ======================= 
//*** Repositories 


//*** Services 
builder.Services.AddTransient<IServiceUsuario, ServiceUsuario>();
builder.Services.AddScoped<IRepositoryUsuario, RepositoryUsuario>();

builder.Services.AddTransient<IServiceSubasta, ServiceSubasta>();
builder.Services.AddScoped<IRepositorySubasta, RepositorySubasta>();

builder.Services.AddTransient<IServicePuja, ServicePuja>();
builder.Services.AddScoped<IRepositoryPuja, RepositoryPuja>();

builder.Services.AddTransient<IServiceObjeto, ServiceObjeto>();
builder.Services.AddScoped<IRepositoryObjeto, RepositoryObjeto>();


// ======================= 
// Configurar AutoMapper 
// ======================= 
builder.Services.AddAutoMapper(config =>
{
    //*** Profiles 
    config.AddProfile<UsuarioProfile>();
    config.AddProfile<SubastaProfile>();
    config.AddProfile<PujaProfile>();
    config.AddProfile<ObjetoProfile>();
});

// ======================= 
// Configurar SQL Server DbContext 
// ======================= 
var connectionString = builder.Configuration.GetConnectionString("SqlServerDataBase");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "No se encontró la cadena de conexión 'SqlServerDataBase' en appsettings.json appsettings.Development.json." ); 
}

builder.Services.AddDbContext<SubastaContext>(options =>
{
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        // Reintentos ante fallos transitorios (recomendado) 
        sqlOptions.EnableRetryOnFailure();
    });

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

//*********** 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, 
    //see https://aka.ms/aspnetcore-hsts. 
    app.UseHsts();
}
else
{
    // Middleware personalizado 
    app.UseMiddleware<ErrorHandlingMiddleware>();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSerilogRequestLogging();

app.UseAuthorization();
app.UseAntiforgery();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
