FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["RediSearchCore/RediSearchCore.csproj", "RediSearchCore/"]
COPY ["RediSearchCore.Infrastructure/RediSearchCore.Infrastructure.csproj", "RediSearchCore.Infrastructure/"]
COPY ["RediSearchCore.Core/RediSearchCore.Core.csproj", "RediSearchCore.Core/"]
RUN dotnet restore "RediSearchCore/RediSearchCore.csproj"
COPY . .
WORKDIR "/src/RediSearchCore"
RUN dotnet build "RediSearchCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RediSearchCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RediSearchCore.dll"]