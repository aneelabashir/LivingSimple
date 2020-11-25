FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["BalanceManagement/BalanceManagement.csproj", "BalanceManagement/"]
RUN dotnet restore "BalanceManagement/BalanceManagement.csproj"
COPY . .
WORKDIR "/src/BalanceManagement"
RUN dotnet build "BalanceManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BalanceManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BalanceManagement.dll"]