namespace StancaBankApi.Data.DTOs;

public class CustomerSummaryDto
{
    public int CustomerId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int AccountCount { get; set; }
    public decimal TotalBalance { get; set; }
}
