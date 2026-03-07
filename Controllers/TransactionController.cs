namespace StancaBankApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Customer")]
public class TransactionController(ITransactionService transactionService) : ControllerBase
{
    [HttpGet("account/{accountId:int}")]
    public ActionResult<List<TransactionDTO>> GetTransactions(int accountId)
    {
        if (!User.TryGetCustomerId(out var customerId))
        {
            return Unauthorized();
        }

        try
        {
            return Ok(transactionService.GetByAccountId(customerId, accountId));
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("transfer")]
    public ActionResult<TransactionDTO> Transfer([FromBody] TransferDTO dto)
    {
        if (!User.TryGetCustomerId(out var customerId))
        {
            return Unauthorized();
        }

        try
        {
            return Ok(transactionService.Transfer(customerId, dto));
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
