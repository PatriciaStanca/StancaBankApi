namespace StancaBankApi.Data.DTOs;

public class CreateAccountDto
{
    [Required]
    public int AccountTypeId { get; set; }
}

public class CreateAccountDTO : CreateAccountDto;
