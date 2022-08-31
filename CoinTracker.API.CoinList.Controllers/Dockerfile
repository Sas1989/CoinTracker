#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoinTracker.API.CoinList.Controllers/CoinTracker.API.CoinList.Controllers.csproj", "CoinTracker.API.CoinList.Controllers/"]
COPY ["CoinTracker.Api.CoinList.Infrastructure/CoinTracker.Api.CoinList.Infrastructure.csproj", "CoinTracker.Api.CoinList.Infrastructure/"]
COPY ["CoinTracker.API.CoinList.Application/CoinTracker.API.CoinList.Application.csproj", "CoinTracker.API.CoinList.Application/"]
COPY ["CoinTracker.API.CoinList.Domain/CoinTracker.API.CoinList.Domain.csproj", "CoinTracker.API.CoinList.Domain/"]
RUN dotnet restore "CoinTracker.API.CoinList.Controllers/CoinTracker.API.CoinList.Controllers.csproj"
COPY . .
WORKDIR "/src/CoinTracker.API.CoinList.Controllers"
RUN dotnet build "CoinTracker.API.CoinList.Controllers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoinTracker.API.CoinList.Controllers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoinTracker.API.CoinList.Controllers.dll"]