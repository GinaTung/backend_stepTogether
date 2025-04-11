using Microsoft.EntityFrameworkCore;
using stepTogether.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<StepTogetherDbContext>(options =>
    options.UseNpgsql(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 註冊 CORS 策略
builder.Services.AddCors(op =>
{
    op.AddPolicy("WISE_CORS", set =>
    {
        set.SetIsOriginAllowed(origin => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("http://localhost:3000", "https://backend-steptogether.onrender.com");  // 支持本地开发和生产
    });
});

// 註冊服務
builder.Services.AddControllers();

var app = builder.Build();

// 啟用 Swagger UI
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // 本地环境（开发环境）和生产环境（部署环境）的Swagger路径
    var swaggerUrl = builder.Environment.IsDevelopment()
        ? "/swagger/v1/swagger.json"
        : "https://backend-steptogether.onrender.com/swagger/v1/swagger.json";

    options.SwaggerEndpoint(swaggerUrl, "My API V1");
});

app.UseHttpsRedirection();

// 啟用 CORS
app.UseCors("WISE_CORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
