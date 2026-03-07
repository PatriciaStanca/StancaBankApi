namespace StancaBankApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Customer")]
public class LoanController(ILoanService loanService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<LoanDTO>> GetMyLoans()
    {
        if (!User.TryGetCustomerId(out var customerId))
        {
            return Unauthorized();
        }

        return Ok(loanService.GetByCustomerId(customerId));
    }
}
