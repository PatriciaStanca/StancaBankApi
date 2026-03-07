namespace StancaBankApi.Data.DTOs;

public class DispositionDTO
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int AccountId { get; set; }
    public string Role { get; set; } = string.Empty;
}
