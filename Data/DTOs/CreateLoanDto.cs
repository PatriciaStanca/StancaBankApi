namespace StancaBankApi.Data.DTOs;

public class CreateLoanDto
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int DepositAccountId { get; set; }

    [Range(1, double.MaxValue)]
    public decimal Amount { get; set; }

    [Range(0, 100)]
    public decimal InterestRate { get; set; }
}

public class CreateLoanDTO : CreateLoanDto;
