FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./src", "."]
RUN dotnet restore "./RentAMotto.Deliverers.Api/RentAMotto.Deliverers.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./RentAMotto.Deliverers.Api/RentAMotto.Deliverers.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RentAMotto.Deliverers.Api/RentAMotto.Deliverers.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentAMotto.Deliverers.Api.dll"]