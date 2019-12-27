#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_ENVIRONMENT=Production

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CoolCopter.Api/CoolCopter.Api.csproj", "CoolCopter.Api/"]
RUN dotnet restore "CoolCopter.Api/CoolCopter.Api.csproj"
COPY . .
WORKDIR "/src/CoolCopter.Api"
RUN dotnet build "CoolCopter.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoolCopter.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoolCopter.Api.dll"]