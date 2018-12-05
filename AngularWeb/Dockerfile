FROM microsoft/dotnet:2.1-runtime-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY AngularWeb/AngularWeb.csproj AngularWeb/
RUN dotnet restore AngularWeb/AngularWeb.csproj
COPY . .
WORKDIR /src/AngularWeb
RUN dotnet build AngularWeb.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AngularWeb.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AngularWeb.dll"]
