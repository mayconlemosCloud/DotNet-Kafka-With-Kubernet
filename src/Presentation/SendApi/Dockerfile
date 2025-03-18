# Use a .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the entire solution directory and restore dependencies
COPY . . 
RUN dotnet restore ./SendApi.csproj

# Build the application to verify it compiles successfully
RUN dotnet build ./SendApi.csproj -c Release

# Publish the application
RUN dotnet publish ./SendApi.csproj -c Release -o out

# Use a runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "SendApi.dll"]
