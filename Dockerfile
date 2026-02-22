# Build stage
# This is base image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build  
# This sets the working directory to /app
WORKDIR /app

# Copy solution and project files
# This copies the solution file to the working directory
COPY LIS.sln ./
# This copies the project file to the working directory
COPY src/LIS/LIS.csproj src/LIS/  
# This copies the test project file to the working directory
COPY tests/LIS.Tests/LIS.Tests.csproj tests/LIS.Tests/  

# Restore dependencies
# This downloads all the NuGet packages required by the project
RUN dotnet restore  

# Copy everything else
# This copies all the files from the current directory to the working directory
COPY . .

# Build and publish the main project
# This builds and publishes the main project
RUN dotnet publish src/LIS/LIS.csproj -c Release -o /app/publish  

# Runtime stage
# This is base image for running the application
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
# This sets the working directory to /app
WORKDIR /app  
# Copy files From Stage 1 (named "build"), The folder where we compiled the app in Stage 1 Into the current directory (/app) in this new image
COPY --from=build /app/publish .  
# This sets the entry point for the container
ENTRYPOINT ["dotnet", "LIS.dll"]  
