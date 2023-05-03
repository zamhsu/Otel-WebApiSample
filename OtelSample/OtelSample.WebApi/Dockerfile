FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OtelSample.WebApi/OtelSample.WebApi.csproj", "OtelSample.WebApi/"]
RUN dotnet restore "OtelSample.WebApi/OtelSample.WebApi.csproj"
COPY . .
WORKDIR "/src/OtelSample.WebApi"
RUN dotnet build "OtelSample.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OtelSample.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OtelSample.WebApi.dll"]
