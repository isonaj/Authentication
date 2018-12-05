FROM microsoft/dotnet:2.1-runtime-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY API1/API1.csproj API1/
RUN dotnet restore API1/API1.csproj
COPY . .
WORKDIR /src/API1
RUN dotnet build API1.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish API1.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "API1.dll"]
