#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Wallets/src/API.Wallets/API.Wallets.csproj", "Wallets/src/API.Wallets/"]
COPY ["Wallets/src/API.Wallets.Infrastructure/API.Wallets.Infrastructure.csproj", "Wallets/src/API.Wallets.Infrastructure/"]
COPY ["Wallets/src/API.Wallets.Domain/API.Wallets.Domain.csproj", "Wallets/src/API.Wallets.Domain/"]
COPY ["SDK/src/CoinTracker.API.SDK.Domain/API.SDK.Domain.csproj", "SDK/src/CoinTracker.API.SDK.Domain/"]
COPY ["SDK/src/CoinTracker.API.SDK.Infrastructure/API.SDK.Infrastructure.csproj", "SDK/src/CoinTracker.API.SDK.Infrastructure/"]
COPY ["SDK/src/CoinTracker.API.SDK.Application/API.SDK.Application.csproj", "SDK/src/CoinTracker.API.SDK.Application/"]
COPY ["Wallets/src/API.Wallets.Application/API.Wallets.Application.csproj", "Wallets/src/API.Wallets.Application/"]
RUN dotnet restore "Wallets/src/API.Wallets/API.Wallets.csproj"
COPY . .
WORKDIR "/src/Wallets/src/API.Wallets"
RUN dotnet build "API.Wallets.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.Wallets.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.Wallets.dll"]