FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt install curl

ENV ASPNETCORE_Kestrel__Certificates__Default__Path "/etc/certs/certificate.pfx"
ENV ASPNETCORE_Kestrel__Certificates__Default__Password ""

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WhereHasMyTimeGone.API.Gateway/WhereHasMyTimeGone.API.Gateway.csproj", "WhereHasMyTimeGone.API.Gateway/"]
COPY ["src/WhereHasMyTimeGone.API.Application/WhereHasMyTimeGone.API.Application.csproj", "WhereHasMyTimeGone.API.Application/"]
COPY ["src/WhereHasMyTimeGone.API.Domain/WhereHasMyTimeGone.API.Domain.csproj", "WhereHasMyTimeGone.API.Domain/"]
COPY ["src/WhereHasMyTimeGone.API.Infrastructure/WhereHasMyTimeGone.API.Infrastructure.csproj", "WhereHasMyTimeGone.API.Infrastructure/"]
RUN dotnet restore "WhereHasMyTimeGone.API.Gateway/WhereHasMyTimeGone.API.Gateway.csproj"
COPY ./src ./
WORKDIR "/src/WhereHasMyTimeGone.API.Gateway"
RUN dotnet build "WhereHasMyTimeGone.API.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WhereHasMyTimeGone.API.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

HEALTHCHECK --interval=60s --timeout=3s --retries=3 CMD /usr/bin/curl -f https://localhost/api/health || exit 1
ENTRYPOINT ["dotnet", "WhereHasMyTimeGone.API.Gateway.dll"]
