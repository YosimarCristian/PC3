# Imagen base con runtime ASP.NET Core 9.0 + paquetes para SQLite
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

RUN apt-get update \
    && apt-get install -y libsqlite3-0 \
    && rm -rf /var/lib/apt/lists/*

EXPOSE 80

# Imagen SDK para construir la app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar solo el archivo de proyecto y restaurar dependencias
COPY PC3SALAZAR/PC3SALAZAR.csproj ./ 
RUN dotnet restore "PC3SALAZAR.csproj"

# Copiar todo el código fuente y publicar en modo Release
COPY PC3SALAZAR/. ./ 
RUN dotnet publish "PC3SALAZAR.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final para ejecutar la app publicada
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PC3SALAZAR.dll"]
