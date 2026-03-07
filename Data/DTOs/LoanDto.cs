namespace StancaBankApi.Data.DTOs;

public class LoanDTO
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int DepositAccountId { get; set; }
    public decimal Amount { get; set; }
    public decimal InterestRate { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
