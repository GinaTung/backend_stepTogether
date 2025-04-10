# 使用官方 .NET SDK 作為基礎鏡像
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# 使用 .NET SDK 來建構應用程式
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY stepTogether/stepTogether.csproj /src/stepTogether/
RUN dotnet restore "stepTogether/stepTogether.csproj"
COPY . . 
WORKDIR "/src/stepTogether"
RUN dotnet build "stepTogether.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "stepTogether.csproj" -c Release -o /app/publish

# 從已經構建好的映像來運行應用程式
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "stepTogether.dll"]
