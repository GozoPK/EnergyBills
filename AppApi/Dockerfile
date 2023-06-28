FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# copy everything else in order to build
COPY . ./
RUN dotnet publish -c Release -o out

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS http://*:5001
ENTRYPOINT [ "dotnet", "AppApi.dll" ]

# docker run -e MYSQL_ROOT_PASSWORD=secret -p {{ip address}}:3306:3306 --name mariadb -d mariadb