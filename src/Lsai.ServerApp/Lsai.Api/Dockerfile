#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 7000
EXPOSE 7001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Lsai.Api/Lsai.Api.csproj", "Lsai.Api/"]
COPY ["Lsai.Infrastructure/Lsai.Infrastructure.csproj", "Lsai.Infrastructure/"]
COPY ["Lsai.Application/Lsai.Application.csproj", "Lsai.Application/"]
COPY ["Lsai.Domain/Lsai.Domain.csproj", "Lsai.Domain/"]
COPY ["Lsai.Persistence/Lsai.Persistence.csproj", "Lsai.Persistence/"]
RUN dotnet restore "./Lsai.Api/./Lsai.Api.csproj"
COPY . .
WORKDIR "/src/Lsai.Api"
RUN dotnet build "./Lsai.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lsai.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lsai.Api.dll"]