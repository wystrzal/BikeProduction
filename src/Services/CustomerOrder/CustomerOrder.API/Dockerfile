#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/CustomerOrder/CustomerOrder.API/CustomerOrder.API.csproj", "src/Services/CustomerOrder/CustomerOrder.API/"]
COPY ["src/Services/CustomerOrder/CustomerOrder.Infrastructure/CustomerOrder.Infrastructure.csproj", "src/Services/CustomerOrder/CustomerOrder.Infrastructure/"]
COPY ["src/Services/CustomerOrder/CustomerOrder.Core/CustomerOrder.Core.csproj", "src/Services/CustomerOrder/CustomerOrder.Core/"]
COPY ["src/Services/CustomerOrder/CustomerOrder.Application/CustomerOrder.Application.csproj", "src/Services/CustomerOrder/CustomerOrder.Application/"]
RUN dotnet restore "src/Services/CustomerOrder/CustomerOrder.API/CustomerOrder.API.csproj"
COPY . .
WORKDIR "/src/src/Services/CustomerOrder/CustomerOrder.API"
RUN dotnet build "CustomerOrder.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerOrder.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerOrder.API.dll"]