# 使用官方 .NET SDK 作為基礎鏡像
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# 使用 .NET SDK 來建構應用程式
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 複製 stepTogether.csproj 到容器中的 /src/stepTogether/
COPY ./stepTogether.csproj /src/stepTogether/

# 執行 dotnet restore 來還原專案依賴
RUN dotnet restore /src/stepTogether/stepTogether.csproj

# 複製專案檔案到容器中
COPY . .

# 設定工作目錄為 /src/stepTogether
WORKDIR /src/stepTogether

# 建構專案並輸出到 /app/build 目錄
RUN dotnet build "stepTogether.csproj" -c Release -o /app/build

# 進行發佈，並輸出到 /app/publish 目錄
FROM build AS publish
RUN dotnet publish "stepTogether.csproj" -c Release -o /app/publish

# 從已經構建好的映像來運行應用程式
FROM base AS final
WORKDIR /app

# 複製發佈的檔案
COPY --from=publish /app/publish . 

# 設定容器啟動命令
ENTRYPOINT ["dotnet", "stepTogether.dll"]
