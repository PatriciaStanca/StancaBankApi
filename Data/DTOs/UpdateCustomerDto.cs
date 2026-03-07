namespace StancaBankApi.Data.DTOs;

public class UpdateCustomerDto
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    [Required]
    public string GivenName { get; set; } = string.Empty;

    [Required]
    public string Surname { get; set; } = string.Empty;

    [MinLength(6)]
    public string? Password { get; set; }
}

public class UpdateCustomerDTO : UpdateCustomerDto;
