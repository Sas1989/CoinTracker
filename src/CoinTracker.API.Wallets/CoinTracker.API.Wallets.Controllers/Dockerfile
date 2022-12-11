#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CoinTracker.API.Wallets/CoinTracker.API.Wallets.Controllers/CoinTracker.API.Wallets.csproj", "src/CoinTracker.API.Wallets/CoinTracker.API.Wallets.Controllers/"]
RUN dotnet restore "src/CoinTracker.API.Wallets/CoinTracker.API.Wallets.Controllers/CoinTracker.API.Wallets.csproj"
COPY . .
WORKDIR "/src/src/CoinTracker.API.Wallets/CoinTracker.API.Wallets.Controllers"
RUN dotnet build "CoinTracker.API.Wallets.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoinTracker.API.Wallets.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoinTracker.API.Wallets.dll"]