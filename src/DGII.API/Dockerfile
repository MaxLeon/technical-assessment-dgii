FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.sln .
COPY src/DGII.Domain/DGII.Domain.csproj src/DGII.Domain/
COPY src/DGII.Application/DGII.Application.csproj src/DGII.Application/
COPY src/DGII.Infrastructure/DGII.Infrastructure.csproj src/DGII.Infrastructure/
COPY src/DGII.API/DGII.API.csproj src/DGII.API/
RUN dotnet restore src/DGII.API/DGII.API.csproj

COPY src/ src/
RUN dotnet publish src/DGII.API/DGII.API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "DGII.API.dll"]
