using MaximPractice.API.Dto;
using MaximPractice.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaximPractice.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : Controller
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> GetDriver([FromBody] OrderDto request)
    {
        try
        {
            var result = await _orderService.GetDriverForOrder(
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
