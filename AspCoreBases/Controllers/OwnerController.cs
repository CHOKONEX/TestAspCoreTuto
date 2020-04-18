using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
