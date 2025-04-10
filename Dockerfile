# 使用 .NET SDK 映像來構建應用程式
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 複製專案檔案
COPY . .

# 還原 NuGet 套件
RUN dotnet restore

# 編譯並發佈應用程式
RUN dotnet publish -c Release -o /out

# 使用 .NET ASP.NET 映像來運行應用程式
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# 從構建階段複製已發佈的應用程式檔案
COPY --from=build /out .

# 設定容器啟動時運行的指令（修改為正確的 DLL 名稱）
ENTRYPOINT ["dotnet", "stepTogether.dll"]

# 開放容器的 80 端口
EXPOSE 80
