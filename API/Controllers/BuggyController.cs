using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseController
{
    [HttpGet("unauthorized")]
    public ActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFound()
    {
        return NotFound();
    }

    [HttpGet("internalerror")]
    public ActionResult GetServerError()
    {
        throw new Exception("This is a internal error");
    }
    
    [HttpPost("validationerror")]
    public ActionResult GetValidationError(CreateProductDto product)
    {
        return Ok();
    }
}
