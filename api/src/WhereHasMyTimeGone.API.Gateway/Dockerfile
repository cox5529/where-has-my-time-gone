FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_Kestrel__Certificates__Default__Path "/etc/certs/certificate.pfx"
ENV ASPNETCORE_Kestrel__Certificates__Default__Password ""

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Quacker.API.Gateway/Quacker.API.Gateway.csproj", "Quacker.API.Gateway/"]
COPY ["src/Quacker.API.Application/Quacker.API.Application.csproj", "Quacker.API.Application/"]
COPY ["src/Quacker.API.Domain/Quacker.API.Domain.csproj", "Quacker.API.Domain/"]
COPY ["src/Quacker.API.Infrastructure/Quacker.API.Infrastructure.csproj", "Quacker.API.Infrastructure/"]
RUN dotnet restore "Quacker.API.Gateway/Quacker.API.Gateway.csproj"
COPY ./src ./
WORKDIR "/src/Quacker.API.Gateway"
RUN dotnet build "Quacker.API.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Quacker.API.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Quacker.API.Gateway.dll"]
