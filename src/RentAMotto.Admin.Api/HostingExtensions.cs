using MassTransit;
using Microsoft.EntityFrameworkCore;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Create;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Delete;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Get;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Search;
using RentAMotto.Admin.Application.UseCases.Motorcycle.Update;
using RentAMotto.Common.Api.Configurations;
using RentAMotto.Domain.Repositories;
using RentAMotto.Infrastructure.Persistence.PostgreSql;
using RentAMotto.Infrastructure.Persistence.PostgreSql.Repositories;
using System.Text.Json.Serialization;

namespace Microsoft.DependencyInjection.Extensions;

public static class HostingExtensions
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        builder.Services
            .AddUseCases()
            .AddRepositories(builder.Configuration)
            .AddCustomResponseCompression()
            .AddCustomVersioning();

        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(builder.Configuration["RabbitMQ:Host"]!, h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });

        builder.Services.AddMemoryCache();
        builder.Services.AddHealthChecks();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateMotorcycleUsecase, CreateMotorcycleUsecase>();
        services.AddScoped<ISearchMotorcycleUsecase, SearchMotorcycleUsecase>();
        services.AddScoped<IGetMotorcycleUsecase, GetMotorcycleUsecase>();
        services.AddScoped<IUpdateMotorcycleUsecase, UpdateMotorcycleUsecase>();
        services.AddScoped<IDeleteMotorcycleUsecase, DeleteMotorcycleUsecase>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MottoContext>(options =>
            options.UseNpgsql(configuration["PostgreSQL:ConnectionString"]));

        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IRentalContractRepository, RentalContractRepository>();
        services.AddScoped<IRentalPlanRepository, RentalPlanRepository>();
        services.AddScoped<IDeliveryDriverRepository, DeliveryDriverRepository>();

        return services;
    }

    public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetService<MottoContext>();
        await context.Database.MigrateAsync();
    }
}
