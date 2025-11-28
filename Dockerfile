# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Bind to Render PORT
ENV ASPNETCORE_URLS=http://+:$PORT

# Start your application
ENTRYPOINT ["dotnet", "BloodDonation.dll"]
