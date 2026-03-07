namespace StancaBankApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Customer")]
public class AccountController(IAccountService accountService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<AccountDTO>> GetMyAccounts()
    {
        if (!User.TryGetCustomerId(out var customerId))
        {
            return Unauthorized();
        }

        return Ok(accountService.GetCustomerAccounts(customerId));
    }

    [HttpPost]
    public ActionResult<AccountDTO> CreateAccount([FromBody] CreateAccountDTO dto)
    {
        if (!User.TryGetCustomerId(out var customerId))
        {
            return Unauthorized();
        }

        try
        {
            return Ok(accountService.CreateAccountForCustomer(customerId, dto.AccountTypeId));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{accountId:int}")]
    public ActionResult<AccountDTO> UpdateAccount(int accountId, [FromBody] UpdateAccountDTO dto)
    {
        if (!User.TryGetCustomerId(out var customerId))
        {
            return Unauthorized();
        }

        try
        {
            return Ok(accountService.UpdateAccountType(customerId, accountId, dto.AccountTypeId));
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { message = ex.Message });
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

    [HttpDelete("{accountId:int}")]
    public IActionResult DeleteAccount(int accountId)
    {
        if (!User.TryGetCustomerId(out var customerId))
        {
            return Unauthorized();
        }

        try
        {
            accountService.DeleteAccount(customerId, accountId);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { message = ex.Message });
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
