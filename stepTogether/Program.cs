using Microsoft.EntityFrameworkCore;
using stepTogether.Data;

var builder = WebApplication.CreateBuilder(args);

// 取得資料庫連線字串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 註冊資料庫上下文
builder.Services.AddDbContext<StepTogetherDbContext>(options =>
    options.UseNpgsql(connectionString));

// 註冊Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 註冊CORS
builder.Services.AddCors(op =>
{
    op.AddPolicy("WISE_CORS", set =>
    {
        set.SetIsOriginAllowed(origin => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:3000", "https://backend-steptogether.onrender.com");
    });
});

// 註冊控制器服務
builder.Services.AddControllers();

var app = builder.Build();

// 啟用Swagger UI
app.UseSwagger();

// 根據當前環境顯示Swagger UI
if (app.Environment.IsDevelopment())
{
    // 開發環境：Swagger 設為 /swagger 頁面
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("https://localhost:7136/swagger/v1/swagger.json", "StepTogether API V1"); // 本地開發環境用相對路徑
    });
}
else
{
    // 部署環境：Swagger 設置在根目錄
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("https://backend-steptogether.onrender.com/swagger/v1/swagger.json", "StepTogether API V1"); // 這是部署環境的完整 URL
        //c.RoutePrefix = string.Empty; // 讓Swagger UI在根目錄顯示
    });
}

// 啟用HTTPS重定向
app.UseHttpsRedirection();

// 啟用CORS
app.UseCors("WISE_CORS");

// 啟用授權
app.UseAuthorization();

// 映射控制器
app.MapControllers();

app.Run();
