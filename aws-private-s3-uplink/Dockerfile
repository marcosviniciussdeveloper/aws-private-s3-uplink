FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["aws-private-s3-uplink/aws-private-s3-uplink.csproj", "aws-private-s3-uplink/"]
RUN dotnet restore "aws-private-s3-uplink/aws-private-s3-uplink.csproj"
COPY . .
WORKDIR "/src/aws-private-s3-uplink"
RUN dotnet build "./aws-private-s3-uplink.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./aws-private-s3-uplink.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aws-private-s3-uplink.dll"]
