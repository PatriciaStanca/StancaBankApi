namespace StancaBankApi.Data.DTOs;

public class CustomerWithAccountCreatedDto
{
    public CustomerDTO Customer { get; set; } = new();
    public AccountDto Account { get; set; } = new();
}
