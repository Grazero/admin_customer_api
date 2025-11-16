using admin_customer_api.Service;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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
        Description = "API Key needed to access the endpoints. Example: 'my-secret-key-123-wwwwssadw'",
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

var app = builder.Build();

// ✅ Configure Swagger UI มาก่อน
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
    });
}

// ✅ ใช้ API Key Middleware แบบเลือก性地 - ข้าม Swagger
app.UseWhen(context => 
    !context.Request.Path.StartsWithSegments("/swagger") &&
    context.Request.Path != "/" &&
    !context.Request.Path.StartsWithSegments("/index.html"),
    appBuilder => 
    {
        appBuilder.UseMiddleware<ApiKeyMiddleware>();
    }
);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();