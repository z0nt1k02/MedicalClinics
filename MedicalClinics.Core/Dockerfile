FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MedicalClinics.Core/MedicalClinics.Core.csproj", "MedicalClinics.Core/"]
RUN dotnet restore "MedicalClinics.Core/MedicalClinics.Core.csproj"
COPY . .
WORKDIR "/src/MedicalClinics.Core"
RUN dotnet build "MedicalClinics.Core.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MedicalClinics.Core.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedicalClinics.Core.dll"]
