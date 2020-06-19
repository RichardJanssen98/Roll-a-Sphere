FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["/PlayerDataService.csproj", "./"]
RUN dotnet restore "PlayerDataService.csproj"
COPY . .
RUN dotnet build "PlayerDataService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayerDataService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayerDataService.dll"]