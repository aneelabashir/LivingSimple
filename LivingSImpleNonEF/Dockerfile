FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["LivingSImpleNonEF/LivingSImpleNonEF.csproj", "LivingSImpleNonEF/"]
RUN dotnet restore "LivingSImpleNonEF/LivingSImpleNonEF.csproj"
COPY . .
WORKDIR "/src/LivingSImpleNonEF"
RUN dotnet build "LivingSImpleNonEF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LivingSImpleNonEF.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LivingSImpleNonEF.dll"]