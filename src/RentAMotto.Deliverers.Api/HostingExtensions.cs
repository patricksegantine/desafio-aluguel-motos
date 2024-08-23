using Microsoft.EntityFrameworkCore;
using RentAMotto.Common.Abstraction.Services;
using RentAMotto.Common.Api.Configurations;
using RentAMotto.Deliverers.Application.UseCases.Deliverers.DrivingLicence;
using RentAMotto.Deliverers.Application.UseCases.Deliverers.Register;
using RentAMotto.Deliverers.Application.UseCases.Motorcycles.Search;
using RentAMotto.Deliverers.Application.UseCases.RentalContracts.Create;
using RentAMotto.Deliverers.Application.UseCases.RentalContracts.Get;
using RentAMotto.Deliverers.Application.UseCases.RentalContracts.GetBalance;
using RentAMotto.Deliverers.Application.UseCases.RentalPlans.Search;
using RentAMotto.Domain.Repositories;
using RentAMotto.Infrastructure.Persistence.PostgreSql;
using RentAMotto.Infrastructure.Persistence.PostgreSql.Repositories;
using RentAMotto.Infrastructure.Storage;
using System.Text.Json.Serialization;

namespace RentAMotto.Deliverers.Api;

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
            .AddExternalServices()
            .AddRepositories(builder.Configuration)
            .AddCustomResponseCompression()
            .AddCustomVersioning();

        builder.Services.AddMemoryCache();
        builder.Services.AddHealthChecks();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterDeliveryDriverUsecase, RegisterDeliveryDriverUsecase>();
        services.AddScoped<IUploadDrivingLicenceUsecase, UploadDrivingLicenceUsecase>();
        services.AddScoped<ICreateRentalContractUsecase, CreateRentalContractUsecase>();
        services.AddScoped<IGetRentalContractUsecase, GetRentalContractUsecase>();
        services.AddScoped<IGetBalanceUsecase, GetBalanceUsecase>();
        services.AddScoped<ISearchRentalPlanUsecase, SearchRentalPlanUsecase>();
        services.AddScoped<ISearchMotorcycleUsecase, SearchMotorcycleUsecase>();

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

    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, LocalStorageService>();

        return services;
    }
}
