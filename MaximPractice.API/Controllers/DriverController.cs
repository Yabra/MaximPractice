using MaximPractice.API.Dto;
using MaximPractice.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaximPractice.API.Controllers;

[ApiController]
[Route("api/drivers")]
public class DriverController : ControllerBase
{
    private readonly DriverService _driverService;

    public DriverController(DriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpPost]
    public IActionResult AddOrUpdate([FromBody] DriverPositionDto request)
    {
        try
        {

            var result = _driverService.AddOrUpdateDriver(
                request.Id,
                request.X,
                request.Y);

            return Ok(result);
        }

        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
