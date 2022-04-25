#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ActivityMe.Groups.API/ActivityMe.Groups.API.csproj", "ActivityMe.Groups.API/"]
COPY ["ActivityMe.Common/ActivityMe.Common.csproj", "ActivityMe.Common/"]
RUN dotnet restore "ActivityMe.Groups.API/ActivityMe.Groups.API.csproj"
COPY . .
WORKDIR "/src/ActivityMe.Groups.API"
RUN dotnet build "ActivityMe.Groups.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ActivityMe.Groups.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ActivityMe.Groups.API.dll"]
