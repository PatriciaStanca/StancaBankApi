namespace StancaBankApi.Data.DTOs;

public class TransferByAccountNumberDto
{
    [Required]
    public int FromAccountId { get; set; }

    [Required]
    public string ToAccountNumber { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    public string Description { get; set; } = string.Empty;
}
