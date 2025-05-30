# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CleanArchMediatR.Template.Api/CleanArchMediatR.Template.Api.csproj", "src/CleanArchMediatR.Template.Api/"]
COPY ["src/CleanArchMediatR.Template.Application/CleanArchMediatR.Template.Application.csproj", "src/CleanArchMediatR.Template.Application/"]
COPY ["src/CleanArchMediatR.Template.Domain/CleanArchMediatR.Template.Domain.csproj", "src/CleanArchMediatR.Template.Domain/"]
COPY ["src/CleanArchMediatR.Template.Shared/CleanArchMediatR.Template.Shared.csproj", "src/CleanArchMediatR.Template.Shared/"]
COPY ["src/CleanArchMediatR.Template.Infra/CleanArchMediatR.Template.Infra.csproj", "src/CleanArchMediatR.Template.Infra/"]
COPY ["src/CleanArchMediatR.Template.PersistenceFactory/CleanArchMediatR.Template.PersistenceFactory.csproj", "src/CleanArchMediatR.Template.PersistenceFactory/"]
RUN dotnet restore "./src/CleanArchMediatR.Template.Api/CleanArchMediatR.Template.Api.csproj"
COPY . .
WORKDIR "/src/src/CleanArchMediatR.Template.Api"
RUN dotnet build "./CleanArchMediatR.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CleanArchMediatR.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchMediatR.Template.Api.dll"]