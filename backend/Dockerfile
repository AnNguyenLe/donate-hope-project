# Use the official .NET 8 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the solution file
COPY DonateHope.sln ./

# Copy the project files for all layers
COPY src/DonateHope.Core/DonateHope.Core.csproj src/DonateHope.Core/
COPY src/DonateHope.Domain/DonateHope.Domain.csproj src/DonateHope.Domain/
COPY src/DonateHope.Infrastructure/DonateHope.Infrastructure.csproj src/DonateHope.Infrastructure/
COPY src/DonateHope.WebAPI/DonateHope.WebAPI.csproj src/DonateHope.WebAPI/

# Restore dependencies
RUN dotnet restore DonateHope.sln

# Copy the source code
COPY src/ ./src/

# Build and publish the WebAPI project
WORKDIR /src/src/DonateHope.WebAPI
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 8 ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Expose the port the app runs on
EXPOSE 7066

# Set the ASP.NET Core URLs
ENV ASPNETCORE_URLS=http://+:7066

# Set the entry point
ENTRYPOINT ["dotnet", "DonateHope.WebAPI.dll"]