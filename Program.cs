using admin_customer_api.Service;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpOverrides; // 💡 ต้องเพิ่ม using นี้
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 💡 NEW: ตั้งค่าให้เชื่อถือ Header จาก Proxy (สำคัญมากสำหรับ Docker/NPM)
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = 
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    // เนื่องจากการใช้ Docker/NPM เราอาจไม่จำเป็นต้องระบุ IP ของ Proxy
    // แต่ถ้ายังไม่ได้ผล ให้ลองระบุ Networks ของ Docker Host IP ในภายหลัง
});

builder.Services.AddScoped<IDcServices, DC_service>();

// ✅ Add Swagger Services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Customer API", 
        Version = "v1",
        Description = "API สำหรับจัดการข้อมูลลูกค้า"
    });
    
    // ✅ Add API Key Support to Swagger
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key needed to access the endpoints. Example: 'didyouknow-lekchaiyawit-bro'",
        Type = SecuritySchemeType.ApiKey,
        Name = "X-API-Key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { scheme, new List<string>() }
    });
});

// เพิ่ม CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
         policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// ✅ ใช้ CORS ต้องอยู่ลำดับแรกๆ
app.UseCors("AllowAll");

// ✅ แสดง Swagger ในทุก Environment
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
    c.RoutePrefix = "swagger"; // ทำให้แน่ใจว่า path ถูกต้อง
});

// 💡 NEW: ใช้ Forwarded Headers Middleware
app.UseForwardedHeaders();


// ✅ ใช้ API Key Middleware แบบเลือก性地 - ข้าม Swagger และ root
app.UseWhen(context => 
    !context.Request.Path.StartsWithSegments("/swagger") &&
    context.Request.Path != "/" &&
    context.Request.Path != "/swagger/index.html" &&
    !context.Request.Path.StartsWithSegments("/swagger/v1/swagger.json"),
    appBuilder => 
    {
        appBuilder.UseMiddleware<ApiKeyMiddleware>();
    }
);

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();