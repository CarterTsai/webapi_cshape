using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using webapi.Framework.DAL;

namespace webapi.Middleware
{
    public class MiddlewareTest
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareTest> _logger;
        private readonly IOperationTest _iop;

        public MiddlewareTest(RequestDelegate next, 
                              ILogger<MiddlewareTest> logger,
                              IOperationTest iop)
        {
            _next = next;

            _logger = logger;
            _iop = iop;
            _logger.LogInformation("MiddlewareTest init");
        }
        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("MiddlewareTest");
            _logger.LogInformation(_iop.Get());
            await _next(httpContext);
        }
    }
}