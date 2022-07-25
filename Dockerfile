FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./MediaApp.Api/MediaApp.Api.csproj MediaApp.Api/
COPY ./MediaApp.Application/MediaApp.Application.csproj MediaApp.Application/
COPY ./DataAccess/MediaApp.DataAccess.csproj DataAccess/
COPY ./MediaApp.Infrastructure/MediaApp.Infrastructure.csproj MediaApp.Infrastructure/
COPY ./MediaApp.Domain/MediaApp.Domain.csproj MediaApp.Domain/
COPY ./MediaApp.Common/MediaApp.Common.csproj MediaApp.Common/
RUN dotnet restore MediaApp.Api/MediaApp.Api.csproj
COPY . .
WORKDIR /src/MediaApp.Api

RUN dotnet build ./MediaApp.Api.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish ./MediaApp.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app/MediaApp.Api
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaApp.Api.dll"]