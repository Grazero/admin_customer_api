FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["admin_customer_api.csproj", "."]
RUN dotnet restore "admin_customer_api.csproj"
COPY . .
RUN dotnet build "admin_customer_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "admin_customer_api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "admin_customer_api.dll"]