using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

        public ValuesController(IHostingEnvironment env, 
                                ILogger<ValuesController> logger,
                                IOperationTest iop)
        {
            _env = env;
            _logger = logger;
            _iop = iop;
            System.Console.WriteLine("ValuesController init");
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            System.Console.WriteLine(_iop.Get());
            _logger.LogInformation("test");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
