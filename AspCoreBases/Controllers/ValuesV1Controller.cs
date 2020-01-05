using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TestAspCoreTuto.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/values")]
    [ApiController]
    public class ValuesV1Controller : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value one from api version", "One" };
        }
    }


    [ApiVersion("2.0")]
    [Route("api/values")]
    [ApiController]
    public class ValuesV2Controller : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value two from the api version", "two" };
        }
    }
}
