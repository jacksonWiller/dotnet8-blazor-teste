#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src


COPY ["Clientes.Api/Clientes.Api.csproj", "Clientes.Api/"]
COPY ["Clientes.Aplicacao/Clientes.Aplicacao.csproj", "Clientes.Aplicacao/"]
COPY ["Clientes.Dominio/Clientes.Dominio.csproj", "Clientes.Dominio/"]
COPY ["Clientes.Infra/Clientes.Infra.csproj", "Clientes.Infra/"]

RUN dotnet restore "Clientes.Api/Clientes.Api.csproj"
COPY . .

WORKDIR "Clientes.Api/"
RUN dotnet build "Clientes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Clientes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clientes.Api.dll"]