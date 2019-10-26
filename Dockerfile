FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
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