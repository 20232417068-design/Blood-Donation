# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the solution and project files first (improves caching)
COPY BloodDonation/*.csproj BloodDonation/
RUN dotnet restore BloodDonation/BloodDonation.csproj

# Copy everything
COPY . .

# Publish
RUN dotnet publish BloodDonation/BloodDonation.csproj -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:$PORT

ENTRYPOINT ["dotnet", "BloodDonation.dll"]
