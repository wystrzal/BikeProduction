#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/Delivery/Delivery.API/Delivery.API.csproj", "src/Services/Delivery/Delivery.API/"]
COPY ["src/Services/Delivery/Delivery.Infrastructure/Delivery.Infrastructure.csproj", "src/Services/Delivery/Delivery.Infrastructure/"]
COPY ["src/Shared/HTTP/HttpClient/BikeHttpClient.csproj", "src/Shared/HTTP/HttpClient/"]
COPY ["src/Services/Delivery/Delivery.Core/Delivery.Core.csproj", "src/Services/Delivery/Delivery.Core/"]
COPY ["src/Services/Delivery/Delivery.Application/Delivery.Application.csproj", "src/Services/Delivery/Delivery.Application/"]
RUN dotnet restore "src/Services/Delivery/Delivery.API/Delivery.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Delivery/Delivery.API"
RUN dotnet build "Delivery.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Delivery.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Delivery.API.dll"]