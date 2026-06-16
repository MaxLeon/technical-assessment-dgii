using DGII.API.Filters;
using DGII.API.Middleware;
using DGII.Application.DTOs.Comprobantes;
using DGII.Application.DTOs.Contribuyentes;
using DGII.Application.Interfaces;
using DGII.Application.Services;
using DGII.Application.Validators;
using DGII.Domain.Interfaces;
using DGII.Infrastructure.Data;
using DGII.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Compact;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(new CompactJsonFormatter())
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    Log.Information("Iniciando DGII API.");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddDbContext<AppDbContext>(opts =>
        opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddScoped<IContribuyenteRepository, ContribuyenteRepository>();
    builder.Services.AddScoped<IComprobanteRepository, ComprobanteRepository>();
    builder.Services.AddScoped<IContribuyenteService, ContribuyenteService>();
    builder.Services.AddScoped<IComprobanteService, ComprobanteService>();
    builder.Services.AddScoped<IValidator<ContribuyenteRequestDto>, ContribuyenteValidator>();
    builder.Services.AddScoped<IValidator<ComprobanteRequestDto>, ComprobanteValidator>();

    builder.Services.AddControllers(opts =>
        opts.Filters.Add<ResponseFormatterFilter>());

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddCors(opts =>
        opts.AddDefaultPolicy(policy =>
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod()));

    var app = builder.Build();

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors();
    app.UseAuthorization();
    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
    }

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "DGII API terminó inesperadamente.");
}
finally
{
    await Log.CloseAndFlushAsync();
}
