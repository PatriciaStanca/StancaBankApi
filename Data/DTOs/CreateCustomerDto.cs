namespace StancaBankApi.Data.DTOs;

public class CreateCustomerDto
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}

public class CreateCustomerDTO : CreateCustomerDto;
