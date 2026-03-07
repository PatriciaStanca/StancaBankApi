namespace StancaBankApi.Controllers;

public partial class AdminController
{
    [Authorize(Roles = "Admin")]
    [HttpPost("loans")]
    public ActionResult<LoanDTO> CreateLoan([FromBody] CreateLoanDTO dto)
    {
        try
        {
            return Ok(adminService.CreateLoan(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
