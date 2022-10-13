#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CoinTracker.API.CoinList/CoinTracker.API.CoinList.Controllers/CoinTracker.API.CoinList.Controllers.csproj", "src/CoinTracker.API.CoinList/CoinTracker.API.CoinList.Controllers/"]
COPY ["src/CoinTracker.API.CoinList/CoinTracker.Api.CoinList.Infrastructure/CoinTracker.Api.CoinList.Infrastructure.csproj", "src/CoinTracker.API.CoinList/CoinTracker.Api.CoinList.Infrastructure/"]
COPY ["src/CoinTracker.API.CoinList/CoinTracker.API.CoinList.Application/CoinTracker.API.CoinList.Application.csproj", "src/CoinTracker.API.CoinList/CoinTracker.API.CoinList.Application/"]
COPY ["src/CoinTracker.API.CoinList/CoinTracker.API.CoinList.Domain/CoinTracker.API.CoinList.Domain.csproj", "src/CoinTracker.API.CoinList/CoinTracker.API.CoinList.Domain/"]
RUN dotnet restore "src/CoinTracker.API.CoinList/CoinTracker.API.CoinList.Controllers/CoinTracker.API.CoinList.Controllers.csproj"
COPY . .
WORKDIR "/src/src/CoinTracker.API.CoinList/CoinTracker.API.CoinList.Controllers"
RUN dotnet build "CoinTracker.API.CoinList.Controllers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoinTracker.API.CoinList.Controllers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoinTracker.API.CoinList.Controllers.dll"]
