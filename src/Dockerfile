FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Core/MedicationSystem.Application/MedicationSystem.Application.csproj","Core/MedicationSystem.Application/"]
COPY ["Core/MedicationSystem.Domain/MedicationSystem.Domain.csproj","Core/MedicationSystem.Domain/"]
COPY ["Infrastructure/MedicationSystem.Infrastructure/MedicationSystem.Infrastructure.csproj","Infrastructure/MedicationSystem.Infrastructure/"]
COPY ["Infrastructure/MedicationSystem.Persistence.EF/MedicationSystem.Persistence.EF.csproj","Infrastructure/MedicationSystem.Persistence.EF/"]
COPY ["Presentation/MedicationSystem.RestApi/MedicationSystem.RestApi.csproj","Presentation/MedicationSystem.RestApi/"]
RUN dotnet restore "Presentation/MedicationSystem.RestApi/MedicationSystem.RestApi.csproj"
COPY . .
WORKDIR "/src/Presentation/MedicationSystem.RestApi/"
RUN dotnet build "MedicationSystem.RestApi.csproj" -o /app/build

FROM build AS publish
RUN dotnet publish "MedicationSystem.RestApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedicationSystem.RestApi.dll"]