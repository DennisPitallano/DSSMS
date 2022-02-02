#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["AJ3.WebApp/AJ3.WebApp.csproj", "AJ3.WebApp/"]
RUN dotnet restore "AJ3.WebApp/AJ3.WebApp.csproj"
COPY . .
WORKDIR "/src/AJ3.WebApp"
RUN dotnet build "AJ3.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AJ3.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AJ3.WebApp.dll"]