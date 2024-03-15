FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./InternetBank.Core.Api/InternetBank.Core.Api.csproj", "InternetBank.Core.Api/"]
COPY ["./InternetBank.Core.Domain/InternetBank.Core.Domain.csproj", "InternetBank.Core.Domain/"]
COPY ["./InternetBank.Core.Application/InternetBank.Core.Application.csproj", "InternetBank.Core.Application/"]
COPY ["./InternetBank.Core.Infrastructure/InternetBank.Core.Infrastructure.csproj", "InternetBank.Core.Infrastructure/"]
COPY ["./InternetBank.Core.Persistence/InternetBank.Core.Persistence.csproj", "InternetBank.Core.Persistence/"]

RUN dotnet restore "InternetBank.Core.Api/InternetBank.Core.Api.csproj"
RUN dotnet restore "InternetBank.Core.Application/InternetBank.Core.Application.csproj"
RUN dotnet restore "InternetBank.Core.Domain/InternetBank.Core.Domain.csproj"
RUN dotnet restore "InternetBank.Core.Infrastructure/InternetBank.Core.Infrastructure.csproj"
RUN dotnet restore "InternetBank.Core.Persistence/InternetBank.Core.Persistence.csproj"


COPY . .
WORKDIR "/src/InternetBank.Core.Api"
RUN dotnet build "InternetBank.Core.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InternetBank.Core.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InternetBank.Core.Api.dll"]
