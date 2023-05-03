FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestPoint.WebAPI/TestPoint.WebAPI.csproj", "TestPoint.WebAPI/"]
COPY ["TestPoint.Domain/TestPoint.Domain.csproj", "TestPoint.Domain/"]
COPY ["TestPoint.Application/TestPoint.Application.csproj", "TestPoint.Application/"]
COPY ["TestPoint.Persistance/TestPoint.DAL.csproj", "TestPoint.Persistance/"]
COPY ["TestPoint.EmailService/TestPoint.EmailService.csproj", "TestPoint.EmailService/"]
COPY ["TestPoint.JwtService/TestPoint.JwtService.csproj", "TestPoint.JwtService/"]
COPY ["TestPoint.Cache/TestPoint.Cache.csproj", "TestPoint.Cache/"]
RUN dotnet restore "TestPoint.WebAPI/TestPoint.WebAPI.csproj"
COPY . .
WORKDIR "/src/TestPoint.WebAPI"
RUN dotnet build "TestPoint.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestPoint.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestPoint.WebAPI.dll"]