#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/Basket/Basket.API/Basket.API.csproj", "src/Services/Basket/Basket.API/"]
COPY ["src/Services/Basket/Basket.Application/Basket.Application.csproj", "src/Services/Basket/Basket.Application/"]
COPY ["src/Services/Basket/Basket.Core/Basket.Core.csproj", "src/Services/Basket/Basket.Core/"]
RUN dotnet restore "src/Services/Basket/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Basket/Basket.API"
RUN dotnet build "Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]