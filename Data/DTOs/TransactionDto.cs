namespace StancaBankApi.Data.DTOs;

public class TransactionDto
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Operation { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
    public string? Symbol { get; set; }
    public string? Bank { get; set; }
    public string? Account { get; set; }
}

public class TransactionDTO : TransactionDto;
