namespace StancaBankApi.Data.DTOs;

public class UpdateAccountDto
{
    [Required]
    public int AccountTypeId { get; set; }
}

public class UpdateAccountDTO : UpdateAccountDto;
