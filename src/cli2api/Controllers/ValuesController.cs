using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations.Rules;

namespace cli2api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]

    public FileContentResult DownloadFile()
    {
        return File(
            "hello file"u8.ToArray(), 
            "application/octet-stream");
    }
}
