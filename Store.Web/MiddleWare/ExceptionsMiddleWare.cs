using Store.Service.HandleResponse;
using System.Net;
using System.Reflection;
using System.Text.Json;

namespace Store.Web.MiddleWare
{
    public class ExceptionsMiddleWare
    {
        private readonly ILogger<ExceptionsMiddleWare> _logger;
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        public ExceptionsMiddleWare(RequestDelegate next
        , IHostEnvironment environment
        , ILogger<ExceptionsMiddleWare> logger)
        {
            _next = next;
            _environment = environment;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _environment.IsDevelopment()

                    ? new CustomException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace) :
                    new CustomException((int)HttpStatusCode.InternalServerError, ex.Message);
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
   }
    

