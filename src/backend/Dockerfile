# Base image for ASP.NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Build stage with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["CupcakeDias.API/CupcakeDias.API.csproj", "CupcakeDias.API/"]
COPY ["CupcakeDias.Data/CupcakeDias.Data.csproj", "CupcakeDias.Data/"]
COPY ["CupcakeDias.Shared/CupcakeDias.Shared.csproj", "CupcakeDias.Shared/"]
COPY ["CupcakeDias.Test/CupcakeDias.Test.csproj", "CupcakeDias.Test/"]
RUN dotnet restore "CupcakeDias.API/CupcakeDias.API.csproj"

# Copy all source code and build the application
COPY . .
WORKDIR /src/CupcakeDias.API
RUN dotnet build "CupcakeDias.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "CupcakeDias.API.csproj" -c Release -o /app/publish


# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the .env file to the runtime container
COPY .env /app/.env

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "CupcakeDias.API.dll"]
