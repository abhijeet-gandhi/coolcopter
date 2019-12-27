#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim-arm32v7 AS base
WORKDIR /app

ENV DOTNET_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CoolCopter.Edge/CoolCopter.Edge.csproj", "CoolCopter.Edge/"]
RUN dotnet restore "CoolCopter.Edge/CoolCopter.Edge.csproj"
COPY . .
WORKDIR "/src/CoolCopter.Edge"
RUN dotnet build "CoolCopter.Edge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoolCopter.Edge.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoolCopter.Edge.dll"]