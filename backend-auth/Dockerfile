FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 7300
EXPOSE 7400

COPY ["./InternetBank.Auth.Api/InternetBank.Auth.Api.csproj", "InternetBank.Auth.Api/"]
COPY ["./InternetBank.Auth.Domain/InternetBank.Auth.Domain.csproj", "InternetBank.Auth.Domain/"]
COPY ["./InternetBank.Auth.Application/InternetBank.Auth.Application.csproj", "InternetBank.Auth.Application/"]
COPY ["./InternetBank.Auth.Infrastructure/InternetBank.Auth.Infrastructure.csproj", "InternetBank.Auth.Infrastructure/"]
COPY ["./InternetBank.Auth.Persistence/InternetBank.Auth.Persistence.csproj", "InternetBank.Auth.Persistence/"]

RUN dotnet restore "InternetBank.Auth.Api/InternetBank.Auth.Api.csproj"
RUN dotnet restore "InternetBank.Auth.Application/InternetBank.Auth.Application.csproj"
RUN dotnet restore "InternetBank.Auth.Domain/InternetBank.Auth.Domain.csproj"
RUN dotnet restore "InternetBank.Auth.Infrastructure/InternetBank.Auth.Infrastructure.csproj"
RUN dotnet restore "InternetBank.Auth.Persistence/InternetBank.Auth.Persistence.csproj"


COPY . .
WORKDIR "/src/InternetBank.Auth.Api"
RUN dotnet build "InternetBank.Auth.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InternetBank.Auth.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InternetBank.Auth.Api.dll"]
