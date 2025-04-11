using Microsoft.EntityFrameworkCore;
using stepTogether.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// ✅ 從設定檔抓取連線字串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<StepTogetherDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ 開發與部署都顯示 Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "stepTogether API V1");
    c.RoutePrefix = ""; // 部署後打開首頁就是 Swagger UI
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
