
using Exceptions;
using System.Net;

namespace Transaction.Api.Midllewares;
public class HandlerErrorsMiddleware
{
    private RequestDelegate _next;
    private ILogger<HandlerErrorsMiddleware> _ILogger;

    public HandlerErrorsMiddleware(RequestDelegate next, ILogger<HandlerErrorsMiddleware> ILogger)
    {
        _next = next;
        _ILogger = ILogger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
            if (httpContext.Response.StatusCode > 400 && httpContext.Response.StatusCode < 500)
            {
                throw new KeyNotFoundException("Not Found");
            }

        }
        catch (Exception error)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";
            _ILogger.Log(LogLevel.Error, error.Message);
            switch (error)
            {
                case ArgumentNullException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                   await response.WriteAsJsonAsync($"Oppps... \n the argument {e.Message} is null!");
                    break;
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    await response.WriteAsync("Oppps... \n Page Not Found!");
                    break;
                case UserNotFoundException e:
                    // user didn't found error
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await response.WriteAsJsonAsync("Oppps... \n user didn't found!");
                    break;
                case DbContextException e:
                    // DbContext error
                    response.StatusCode = (int)HttpStatusCode.NotExtended;
                    await response.WriteAsync("Oppps... \n DbContext error!");
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await response.WriteAsync("Oppps... \n we are trying to fix the problem!");
                    break;
            }
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class HandlerErrorsMiddlewareExtensions
{
    public static IApplicationBuilder UseHandlerErrorsMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HandlerErrorsMiddleware>();
    }
}
