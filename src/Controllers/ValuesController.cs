using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
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
           // System.Console.WriteLine("ValuesController init");
        }

        // Post api/values
        [HttpPost]
        public string Get()
        {
            try {
            System.Console.WriteLine(_iop.Get());
          //  _logger.LogInformation("test");

            _distributedCache.SetStringAsync("abc", "123456789");
            var x  = _distributedCache.GetString("abc");
            return x;
            } catch(Exception ex) {
                System.Console.WriteLine("Exception");
                System.Console.WriteLine(ex.Message);
                 return "Error" ;
            }
        }

        // // GET api/values/5
        // [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

    }
}
