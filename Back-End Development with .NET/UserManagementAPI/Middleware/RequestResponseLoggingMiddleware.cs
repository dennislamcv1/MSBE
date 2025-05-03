// Middleware/RequestResponseLoggingMiddleware.cs
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log request method and path
        var method = context.Request.Method;
        var path = context.Request.Path;
        _logger.LogInformation("Incoming Request: {Method} {Path}", method, path);

        // Buffer the response
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context); // Continue to next middleware

        // Log response status code
        var statusCode = context.Response.StatusCode;
        _logger.LogInformation("Response Status Code: {StatusCode}", statusCode);

        // Reset response body
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
    }
}
