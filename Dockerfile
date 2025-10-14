# ----------------------------------------
# Build stage - build the application
# ----------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build

WORKDIR /src

# Copy solution and project files
COPY "Merchant Service.sln" ./
COPY "Merchant Service.csproj" ./

# Restore dependencies
RUN dotnet restore" Merchant Service.sln"

# Copy the rest of the source code
COPY . .


# Publish the app to the /app directory
RUN dotnet publish "Merchant Service.csproj" -c Release -o /app/publish

# ----------------------------------------
# Runtime stage - run the application
# ----------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime

# Set working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish ./

# Copy the SQLite database file (optional — if using prebuilt .db file)
COPY --from=build appdata.db ./appdata.db

# Expose the custom port your app listens on
EXPOSE 5235

# Set environment variable for ASP.NET Core to listen on port 5235
ENV ASPNETCORE_URLS=http://127.0.0.1:5235

# Start the application
ENTRYPOINT ["dotnet", "Merchant Service.dll"]
