#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ActivityMe.Services.API/ActivityMe.Services.API.csproj", "ActivityMe.Services.API/"]
RUN dotnet restore "ActivityMe.Services.API/ActivityMe.Services.API.csproj"
COPY . .
WORKDIR "/src/ActivityMe.Services.API"
RUN dotnet build "ActivityMe.Services.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ActivityMe.Services.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ActivityMe.Services.API.dll"]