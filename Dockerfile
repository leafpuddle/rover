FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-stage
COPY . /app
WORKDIR /app
RUN dotnet build
