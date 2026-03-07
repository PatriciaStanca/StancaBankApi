namespace StancaBankApi.Data.DTOs;

public class CreateCustomerWithAccountDto
{
    [Required]
    public CreateCustomerDto Customer { get; set; } = new();

    [Required]
    public CreateAccountDto Account { get; set; } = new();
}
