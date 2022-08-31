#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CoinTracker/CoinTracker.csproj", "CoinTracker/"]
RUN dotnet restore "CoinTracker/CoinTracker.csproj"
COPY . .
WORKDIR "/src/CoinTracker"
RUN dotnet build "CoinTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoinTracker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoinTracker.dll"]