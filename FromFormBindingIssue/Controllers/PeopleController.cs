using Microsoft.AspNetCore.Mvc;

namespace FromFormBindingIssue.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    [HttpPost]
    public IActionResult Save([FromForm] Person person, [FromForm] Address address) => NoContent();
}
