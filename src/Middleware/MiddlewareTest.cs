using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace webapi.Middleware
{
    public class MiddlewareTest
    {
        private readonly RequestDelegate _next;
        public MiddlewareTest(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            System.Console.WriteLine("MiddlewareTest");
            await _next(httpContext);
        }
    }
}