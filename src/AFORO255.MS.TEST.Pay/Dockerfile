FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY . .
RUN dotnet publish src/AFORO255.MS.TEST.Pay -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS runtime
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_URLS http://*:80
ENV ASPNETCORE_ENVIRONMENT Docker

EXPOSE 80/tcp

ENTRYPOINT ["dotnet", "AFORO255.MS.TEST.Pay.dll"]