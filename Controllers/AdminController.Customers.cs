namespace StancaBankApi.Controllers;

public partial class AdminController
{
    [Authorize(Roles = "Admin")]
    [HttpPost("customers")]
    public ActionResult<CustomerDTO> CreateCustomer([FromBody] CreateCustomerDTO dto)
    {
        try
        {
            return Ok(adminService.CreateCustomer(dto));
        }
        catch (DbUpdateException ex)
        {
            var details = ex.InnerException?.Message ?? ex.Message;
            return BadRequest(new { message = details });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("customers/{customerId:int}")]
    public ActionResult<CustomerDTO> UpdateCustomer(int customerId, [FromBody] UpdateCustomerDTO dto)
    {
        try
        {
            return Ok(adminService.UpdateCustomer(customerId, dto));
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

    [Authorize(Roles = "Admin")]
    [HttpDelete("customers/{customerId:int}")]
    public IActionResult DeleteCustomer(int customerId)
    {
        try
        {
            adminService.DeleteCustomer(customerId);
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
