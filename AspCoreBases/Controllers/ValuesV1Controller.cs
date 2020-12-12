using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        /// <summary>
        /// Retrieves a specific product by unique id
        /// </summary>
        /// <returns>The list of Employees.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/value
        ///     {        
        ///       "id": "Mike",    
        ///     }
        /// </remarks>
        /// <param name="id">Id : string</param>  
        // GET: api/Employee
        [HttpGet("{id}")]
        public IEnumerable<string> Get(string id)
        {
            return new string[] { "value one from api version", id };
        }
    }


    //[ApiVersion("2.0")]
    //[Route("api/values")]
    //[ApiController]
    //public class ValuesV2Controller : ControllerBase
    //{
    //    [HttpGet]
    //    public IEnumerable<string> Get()
    //    {
    //        return new string[] { "value two from the api version", "two" };
    //    }
    //}
}
