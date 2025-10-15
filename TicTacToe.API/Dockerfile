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
COPY ["TicTacToe.API/TicTacToe.API.csproj", "TicTacToe.API/"]
COPY ["TicTacToe.Application/TicTacToe.Application.csproj", "TicTacToe.Application/"]
COPY ["TicTacToe.DataAccess/TicTacToe.DataAccess.csproj", "TicTacToe.DataAccess/"]
COPY ["TicTacToe.Domain/TicTacToe.Domain.csproj","TicTacToe.Domain/"]
COPY ["TicTacToe.Services/TicTacToe.Services.csproj", "TicTacToe.Services/"]
COPY ["TicTacToe.GameModel/TicTacToe.GameModel.csproj", "TicTacToe.GameModel/"]
RUN dotnet restore "TicTacToe.API/TicTacToe.API.csproj"

# Копируем весь исходный код после восстановления зависимостей
COPY . .
WORKDIR "/src/TicTacToe.API"

# Публикация
RUN dotnet publish "TicTacToe.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TicTacToe.API.dll"]
