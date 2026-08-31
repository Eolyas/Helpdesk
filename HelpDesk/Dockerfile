FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Install certificate management tools
RUN apt-get update \
    && apt-get install -y --no-install-recommends ca-certificates \
    && rm -rf /var/lib/apt/lists/*

# Add your organization's trusted root certificate
COPY rootcaCert.pem /usr/local/share/ca-certificates/corporate-root-ca.crt
RUN chmod 644 /usr/local/share/ca-certificates/corporate-root-ca.crt \
    && update-ca-certificates

COPY ["HelpDesk.csproj", "./"]
RUN dotnet restore "HelpDesk.csproj"

COPY . .
RUN dotnet publish "HelpDesk.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "HelpDesk.dll"]