using System;
using System.Text.Json.Serialization;
using Maps.src.Maps.Core.Interfaces;
using Maps.src.Maps.Core.Models;
using Maps.src.Maps.Infrastructure.Data;
using Maps.src.Maps.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Information()
           .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
           .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
           .MinimumLevel.Debug()
           .Enrich.FromLogContext()
           .WriteTo.Async(a => a.Console())
           .WriteTo.Async(a => a.File("logs/lms.txt", rollingInterval: RollingInterval.Day))
           .CreateLogger();
try
{
    builder.Host.UseSerilog();
    builder.Services.AddMemoryCache();
    builder.Services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ConStr") ?? throw new InvalidOperationException("Connection string 'ConStr' not found.")));
    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
    builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngularApp",
            builder => builder.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod());
    });
    // Add services to the container.
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
    builder.Services.AddTransient<IBranchRepo, BranchRepo>();

    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }
    app.UseCors("AllowAngularApp");
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
