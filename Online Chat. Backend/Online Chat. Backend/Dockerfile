FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Online Chat. Backend/Online Chat. Backend.csproj", "Online Chat. Backend/"]
RUN dotnet restore "Online Chat. Backend/Online Chat. Backend.csproj"
COPY . .
WORKDIR "/src/Online Chat. Backend"
RUN dotnet build "Online Chat. Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Online Chat. Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Online Chat. Backend.dll"]
