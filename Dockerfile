# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar dependências
COPY LarColabs.WebApi/LarColabs.WebApi.csproj LarColabs.WebApi/
COPY LarColabs.sln ./
RUN dotnet restore

# Copiar código fonte
COPY . .
WORKDIR /src/LarColabs.WebApi
RUN dotnet publish -c Release -o /app

# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "LarColabs.WebApi.dll"]
