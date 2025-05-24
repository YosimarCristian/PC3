# Imagen base con runtime ASP.NET Core 9.0 + paquetes necesarios
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Instalar paquetes adicionales necesarios para SQLite y otras librerías
RUN apt-get update && apt-get install -y \
    libsqlite3-0 \
    && rm -rf /var/lib/apt/lists/*

EXPOSE 80

# Imagen de build con SDK .NET 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar proyecto y restaurar dependencias
COPY ["PC3SALAZAR.csproj", "./"]
RUN dotnet restore "PC3SALAZAR.csproj"

# Copiar todo el código y publicar en Release
COPY . .
RUN dotnet publish "PC3SALAZAR.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final para correr la app
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PC3SALAZAR.dll"]
