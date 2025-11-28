# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY BloodDonation.sln .

# Copy the project folder
COPY BloodDonation/ BloodDonation/

# Restore dependencies
RUN dotnet restore BloodDonation/BloodDonation.csproj

# Build and publish
RUN dotnet publish BloodDonation/BloodDonation.csproj -c Release -o /app

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "BloodDonation.dll"]
