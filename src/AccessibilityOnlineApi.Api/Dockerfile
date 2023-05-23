FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AccessibilityOnlineApi.csproj", "./"]
RUN dotnet restore "AccessibilityOnlineApi.csproj"
COPY . .
RUN dotnet publish "AccessibilityOnlineApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AccessibilityOnlineApi.dll"]