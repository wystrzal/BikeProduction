#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/Warehouse/Warehouse.API/Warehouse.API.csproj", "src/Services/Warehouse/Warehouse.API/"]
COPY ["src/Services/Warehouse/Warehouse.Application/Warehouse.Application.csproj", "src/Services/Warehouse/Warehouse.Application/"]
COPY ["src/Services/Warehouse/Warehouse.Infrastructure/Warehouse.Infrastructure.csproj", "src/Services/Warehouse/Warehouse.Infrastructure/"]
COPY ["src/Services/Warehouse/Warehouse.Core/Warehouse.Core.csproj", "src/Services/Warehouse/Warehouse.Core/"]
RUN dotnet restore "src/Services/Warehouse/Warehouse.API/Warehouse.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Warehouse/Warehouse.API"
RUN dotnet build "Warehouse.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Warehouse.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Warehouse.API.dll"]