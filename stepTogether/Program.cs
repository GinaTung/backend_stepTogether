using Microsoft.EntityFrameworkCore;
using stepTogether.Data;
using stepTogether.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Newtonsoft.Json;



var builder = WebApplication.CreateBuilder(args);

// 取得資料庫連線字串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 註冊資料庫上下文
builder.Services.AddDbContext<StepTogetherDbContext>(options =>
    options.UseNpgsql(connectionString));

// 註冊 JwtHelper 為 Singleton（全域共享）
builder.Services.AddSingleton<JwtHelper>();

// 設定 Swagger + JWT 支援
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "請輸入 JWT 授權 token，格式為：Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.OperationFilter<AuthorizeCheckOperationFilter>();
    c.EnableAnnotations();
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<UnauthorizedExample>();

// 設定 CORS
builder.Services.AddCors(op =>
{
    op.AddPolicy("WISE_CORS", set =>
    {
        set.WithOrigins("http://localhost:5173", "https://backend-steptogether.onrender.com", "https://ginatung.github.io")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// 先建立暫時的 JwtHelper 來讀取驗證參數
var tempJwtHelper = new JwtHelper(builder.Configuration);

// 註冊 JWT 驗證
builder.Services.AddSingleton<SupabaseService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = tempJwtHelper.GetValidationParameters();
});

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });


var app = builder.Build();

// 啟用Swagger UI
app.UseSwagger();

// 根據當前環境顯示Swagger UI
if (app.Environment.IsDevelopment())
{
    // 開發環境：Swagger 設為 /swagger 頁面
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("https://localhost:7136/swagger/v1/swagger.json", "StepTogether API V1");
        c.InjectStylesheet("/swagger/custom.css"); // ← 這行會加載你剛剛的 CSS
    });
}
else
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("https://backend-steptogether.onrender.com/swagger/v1/swagger.json", "StepTogether API V1");
        //c.RoutePrefix = string.Empty; // 讓Swagger UI在根目錄顯示
    });
}

// 啟用HTTPS重定向
app.UseHttpsRedirection();
// 啟用CORS
app.UseCors("WISE_CORS");
app.UseAuthentication(); // 放在 Authorization 前面
// 啟用授權
app.UseAuthorization();
app.UseStaticFiles();
// 映射控制器
app.MapControllers();
app.Run();
