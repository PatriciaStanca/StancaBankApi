namespace StancaBankApi.Controllers;

public partial class AdminController
{
    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult<LoginResponseDto> Login([FromBody] AdminLoginDto dto)
    {
        var token = adminService.Login(dto);
        if (token is null)
        {
            return Unauthorized();
        }

        return Ok(new LoginResponseDto
        {
            Token = token,
            Role = "Admin",
            ExpiresAtUtc = DateTime.UtcNow
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("change-password")]
    public IActionResult ChangeOwnPassword([FromBody] ChangePasswordDto dto)
    {
        var username = User.FindFirstValue(JwtRegisteredClaimNames.UniqueName);
        if (string.IsNullOrWhiteSpace(username))
        {
            return Unauthorized();
        }

        try
        {
            adminService.ChangeOwnPassword(username, dto);
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
