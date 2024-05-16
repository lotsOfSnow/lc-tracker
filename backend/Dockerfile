FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /base
COPY "." "."
WORKDIR /base/src/LcTracker.Api
RUN dotnet restore
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LcTracker.Api.dll"]
