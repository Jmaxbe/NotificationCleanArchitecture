﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NotificationAPI/NotificationAPI.csproj", "NotificationAPI/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "NotificationAPI/NotificationAPI.csproj"
COPY . .
WORKDIR "/src/NotificationAPI"
RUN dotnet build "NotificationAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationAPI.dll"]
