#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CoinList/src/API.CoinList/API.CoinList.csproj", "CoinList/src/API.CoinList/"]
COPY ["CoinList/src/Api.CoinList.Infrastructure/API.CoinList.Infrastructure.csproj", "CoinList/src/Api.CoinList.Infrastructure/"]
COPY ["Contracts/src/API.Contracts.csproj", "Contracts/src/"]
COPY ["CoinList/src/API.CoinList.Application/API.CoinList.Application.csproj", "CoinList/src/API.CoinList.Application/"]
COPY ["SDK/src/CoinTracker.API.SDK.Application/API.SDK.Application.csproj", "SDK/src/CoinTracker.API.SDK.Application/"]
COPY ["SDK/src/CoinTracker.API.SDK.Domain/API.SDK.Domain.csproj", "SDK/src/CoinTracker.API.SDK.Domain/"]
COPY ["CoinList/src/API.CoinList.Domain/API.CoinList.Domain.csproj", "CoinList/src/API.CoinList.Domain/"]
COPY ["SDK/src/CoinTracker.API.SDK.Infrastructure/API.SDK.Infrastructure.csproj", "SDK/src/CoinTracker.API.SDK.Infrastructure/"]
RUN dotnet restore "CoinList/src/API.CoinList/API.CoinList.csproj"
COPY . .
WORKDIR "/src/CoinList/src/API.CoinList"
RUN dotnet build "API.CoinList.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.CoinList.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.CoinList.dll"]