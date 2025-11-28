# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore dependencies
RUN dotnet restore BloodDonation.csproj

# Build and publish
RUN dotnet publish BloodDonation.csproj -c Release -o /app

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "BloodDonation.dll"]
