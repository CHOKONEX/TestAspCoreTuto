using Microsoft.AspNetCore.Mvc;

namespace TestAspCoreTuto.Controllers
{
    /*
     The most used methods are:
            OK => returns the 200 status code
            NotFound => returns the 404 status code
            BadRequest => returns the 400 status code
            NoContent => returns the 204 status code
            Created, CreatedAtRoute, CreatedAtAction => returns the 201 status code
            Unauthorized => returns the 401 status code
            Forbid => returns the 403 status code
            StatusCode => returns the status code we provide as input

        200 OK Request was successful; body has response.
        201 OK POST or PUT was successful; body has latest representation.
        204 OK DELETE was successful; resource was deleted.
        400 BAD REQUEST The request was invalid or cannot otherwise be served.
        401 UNAUTHORIZED Authorization failed or authentication details not supplied.
        404 NOT FOUND The URI requested or the resource requested doesn’t exist.
        500 Internal Server Error Something very bad happened. Unhandled exceptions lead to this.
    */
    public class OwnerController : Controller
    {
        //private ILogger _logger;
        //private IRepository _repository;

        //public OwnerController(ILogger logger, IRepository repository)
        //{
        //    _logger = logger;
        //    _repository = repository;
        //}

        //[HttpGet]
        //public IActionResult GetAllOwners()
        //{
        //}

        //[HttpGet("{id}", Name = "OwnerById")]
        //public IActionResult GetOwnerById(Guid id)
        //{
        //}

        //[HttpGet("{id}/account")]
        //public IActionResult GetOwnerWithDetails(Guid id)
        //{
        //}

        //[HttpPost]
        //public IActionResult CreateOwner([FromBody]OwnerForCreationDto owner)
        //{
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateOwner(Guid id, [FromBody]OwnerForUpdateDto owner)
        //{
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteOwner(Guid id)
        //{
        //}
    }
}
