FROM microsoft/dotnet:2.1-runtime-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY MVCWeb/MVCWeb.csproj MVCWeb/
RUN dotnet restore MVCWeb/MVCWeb.csproj
COPY . .
WORKDIR /src/MVCWeb
RUN dotnet build MVCWeb.csproj -c Release -o /app 

FROM build AS publish
RUN dotnet publish MVCWeb.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MVCWeb.dll"]
