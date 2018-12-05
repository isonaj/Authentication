FROM microsoft/dotnet:2.1-runtime-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY API2/API2.csproj API2/
RUN dotnet restore API2/API2.csproj
COPY . .
WORKDIR /src/API2
RUN dotnet build API2.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish API2.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "API2.dll"]
