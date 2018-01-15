#FROM microsoft/aspnetcore:2.0 AS base
#RUN mkdir -p /app
#WORKDIR /app
#EXPOSE 80
#
#FROM microsoft/aspnetcore-build:2.0 AS build
#RUN mkdir -p /src
#WORKDIR /src
#COPY *.sln ./
#COPY APIGateway/APIGateway.csproj APIGateway/
#RUN dotnet restore
#COPY . .
#WORKDIR /src/APIGateway
#RUN dotnet build -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "APIGateway.dll"]

FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "APIGateway.dll"]