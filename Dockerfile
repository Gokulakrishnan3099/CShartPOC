# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY Merchant Service.csproj ./
RUN dotnet restore "Merchant Service.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet publish "Merchant Service.csproj" -c Release -o /app/publish --no-restore

# Use the official ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80
EXPOSE 80
# Expose port 5235
EXPOSE 5235

# Set the entrypoint
ENTRYPOINT ["dotnet", "Merchant Service.dll"]
