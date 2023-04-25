#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Ostral.API/Ostral.API.csproj", "Ostral.API/"]
COPY ["Ostral.Infrastructure/Ostral.Infrastructure.csproj", "Ostral.Infrastructure/"]
COPY ["Ostral.Core/Ostral.Core.csproj", "Ostral.Core/"]
COPY ["Ostral.Domain/Ostral.Domain.csproj", "Ostral.Domain/"]
RUN dotnet restore "Ostral.API/Ostral.API.csproj"
COPY . .
WORKDIR "/src/Ostral.API"
RUN dotnet build "Ostral.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ostral.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ostral.API.dll"]