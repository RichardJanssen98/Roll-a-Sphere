FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["/APIGW.csproj", "."]
RUN dotnet restore "APIGW.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "APIGW.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "APIGW.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIGW.dll"]