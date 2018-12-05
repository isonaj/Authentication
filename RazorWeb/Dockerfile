FROM microsoft/dotnet:2.1-runtime-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY RazorWeb/RazorWeb.csproj RazorWeb/
RUN dotnet restore RazorWeb/RazorWeb.csproj
COPY . .
WORKDIR /src/RazorWeb
RUN dotnet build RazorWeb.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RazorWeb.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RazorWeb.dll"]
