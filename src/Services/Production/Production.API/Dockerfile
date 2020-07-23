#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Services/Production/Production.API/Production.API.csproj", "src/Services/Production/Production.API/"]
COPY ["src/Services/Production/Production.Application/Production.Application.csproj", "src/Services/Production/Production.Application/"]
COPY ["src/Services/Production/Production.Core/Production.Core.csproj", "src/Services/Production/Production.Core/"]
COPY ["src/Shared/BaseRepository/BikeBaseRepository/BikeBaseRepository.csproj", "src/Shared/BaseRepository/BikeBaseRepository/"]
COPY ["src/Services/Production/Production.Infrastructure/Production.Infrastructure.csproj", "src/Services/Production/Production.Infrastructure/"]
RUN dotnet restore "src/Services/Production/Production.API/Production.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Production/Production.API"
RUN dotnet build "Production.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Production.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Production.API.dll"]