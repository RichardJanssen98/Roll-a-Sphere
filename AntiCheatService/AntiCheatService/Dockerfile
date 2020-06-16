FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["/AntiCheatService.csproj", "./"]
RUN dotnet restore "AntiCheatService.csproj"
COPY . .
RUN dotnet build "AntiCheatService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AntiCheatService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AntiCheatService.dll"]