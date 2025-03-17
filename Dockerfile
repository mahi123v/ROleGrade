# Use official .NET SDK 8.0 for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RolesGrade.csproj", "./"]
RUN dotnet restore "RolesGrade.csproj"
COPY . .
RUN dotnet build -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Use a smaller runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RolesGrade.dll"]
