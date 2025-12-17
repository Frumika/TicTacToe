# Базовый образ для выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080 
EXPOSE 8080

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем только файлы проектов и восстанавливаем зависимости
COPY ["Backend.API/Backend.API.csproj", "Backend.API/"]
COPY ["Backend.Application/Backend.Application.csproj", "Backend.Application/"]
COPY ["Backend.DataAccess/Backend.DataAccess.csproj", "Backend.DataAccess/"]
COPY ["Backend.Domain/Backend.Domain.csproj", "Backend.Domain/"]
RUN dotnet restore "Backend.API/Backend.API.csproj"

# Копируем весь исходный код после восстановления зависимостей
COPY . .
WORKDIR "/src/Backend.API"
RUN dotnet publish "Backend.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Backend.API.dll"]