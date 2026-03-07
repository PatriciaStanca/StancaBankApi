namespace StancaBankApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<LoginResponseDto> Login([FromBody] LoginDTO dto)
    {
        var token = customerService.Login(dto);
        if (token is null)
        {
            return Unauthorized();
        }

        return Ok(new LoginResponseDto
        {
            Token = token,
            Role = "Customer",
            ExpiresAtUtc = DateTime.UtcNow
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public ActionResult<List<CustomerDTO>> GetAllCustomers() => Ok(customerService.GetAll());

    [Authorize(Roles = "Customer")]
    [HttpPost("change-password")]
    public IActionResult ChangeOwnPassword([FromBody] ChangePasswordDto dto)
    {
        if (!User.TryGetCustomerId(out var customerId))
        {
            return Unauthorized();
        }

        try
        {
            customerService.ChangeOwnPassword(customerId, dto);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
