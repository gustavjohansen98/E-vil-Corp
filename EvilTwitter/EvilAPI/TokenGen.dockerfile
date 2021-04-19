# had to use some sort of patch to match all the packages .. dunno why exactly, but after lot of trial and error it worked out
FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim-amd64 

# Set environment variables
ENV ASPNETCORE_URLS="http://*:5010"
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
WORKDIR /app
ENTRYPOINT ["dotnet", "run"]