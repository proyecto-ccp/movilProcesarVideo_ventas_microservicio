# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

# 🔥 Limpiar bin/obj y posibles conflictos de nombres
RUN rm -rf /src/ServicioProcesarVideo/bin /src/ServicioProcesarVideo/obj

RUN dotnet restore "ServicioProcesarVideo/ServicioProcesarVideo.csproj"
RUN dotnet publish "ServicioProcesarVideo/ServicioProcesarVideo.csproj" -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

# ✅ Ejecuta el nuevo archivo generado: ServicioProcesarVideoApp.dll
ENTRYPOINT ["dotnet", "ServicioProcesarVideoApp.dll"]
