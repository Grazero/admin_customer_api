// API Key Middleware
public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _apiKey;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _apiKey = configuration["ApiSettings:ApiKey"];
    }

 public async Task InvokeAsync(HttpContext context)
{
    // ✅ Whitelist routes ที่ไม่ต้องใช้ API Key
    var publicPaths = new[]
    {
        "/swagger",
        "/",
        "/index.html"
    };

    // ถ้าเป็น public path ข้ามการตรวจสอบ
    if (publicPaths.Any(path => context.Request.Path.StartsWithSegments(path)))
    {
        await _next(context);
        return;
    }

    // ✅ ตรวจสอบ API Key สำหรับ API routes ทั้งหมด
    if (!context.Request.Headers.TryGetValue("X-API-Key", out var extractedApiKey))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsJsonAsync(new 
        {
            status = 401,
            message = "API Key is missing",
            required_header = "X-API-Key",
            example_key = "dwkaodkwaopdkowpakodpawkodx"
        });
        return;
    }

    if (!_apiKey.Equals(extractedApiKey))
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsJsonAsync(new 
        {
            status = 401,
            message = "Invalid API Key",
            your_key = extractedApiKey.ToString()
        });
        return;
    }

    await _next(context);
}

}
