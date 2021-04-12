FROM microsoft/dotnet:latest
# FROM mcr.microsoft.com/dotnet/core/aspnet:5.0-buster-slim AS base
# FROM mcr.microsoft.com/dotnet/core/sdk:5.0-buster-slim AS build

# Set environment variables
ENV ASPNETCORE_URLS="http://*:5000"
ENV ASPNETCORE_ENVIRONMENT="Development"

# Copy files to app directory
COPY . /app

# Set working directory
WORKDIR /app

# Restore NuGet packages
RUN ["dotnet", "restore"]

# Build the app
RUN ["dotnet", "build"]

# Open port
EXPOSE 5010/tcp

# Run the app
ENTRYPOINT ["dotnet", "run"]