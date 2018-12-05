FROM microsoft/dotnet:2.1-runtime-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Auth/Auth.csproj Auth/
RUN dotnet restore Auth/Auth.csproj
COPY . .
WORKDIR /src/Auth
RUN dotnet build Auth.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Auth.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Auth.dll"]
