using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using webapi.Framework.DAL;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IHostingEnvironment _env;

        private readonly IOperationTest _iop;
        private readonly IDistributedCache _distributedCache;

        public ValuesController(IHostingEnvironment env,
                                IDistributedCache distributedCache,
                                ILogger<ValuesController> logger,
                                IOperationTest iop)
        {
            _env = env;
            _logger = logger;
            _iop = iop;
            _distributedCache = distributedCache;
        }

        // Post api/values
        [HttpPost]
        public async Task<string> Get()
        {
            try
            {
                await _distributedCache.SetStringAsync("abc", "123456789");
                var x = _distributedCache.GetString("abc");
                _logger.LogInformation(x);
                return x;
            }
            catch (Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));
                return "Error";
            }
        }
    }
}
