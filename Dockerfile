# Use the .NET 9.0 ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview

# Set the working directory inside the container
WORKDIR /app

# Copy the published .NET app into the container
COPY . .

# Expose your app’s port (change if needed)
EXPOSE 5235

# Optional: set environment variable so ASP.NET knows what port to bind
ENV ASPNETCORE_URLS=http://127.0.0.1:5235

# Run the .NET app
ENTRYPOINT ["dotnet", "Merchant Service.dll"]