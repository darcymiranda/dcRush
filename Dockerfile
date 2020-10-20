FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

COPY ./src/** .

RUN dotnet restore "./TheRush.WebApp.csproj"

RUN dotnet publish "TheRush.WebApp.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "TheRush.WebApp.dll"]