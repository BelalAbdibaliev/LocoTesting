FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/LocoTesting.API/LocoTesting.API.csproj", "src/LocoTesting.API/"]
RUN dotnet restore "src/LocoTesting.API/LocoTesting.API.csproj"
COPY . .
WORKDIR "/src/src/LocoTesting.API"
RUN dotnet build "LocoTesting.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LocoTesting.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LocoTesting.API.dll"]
