namespace StancaBankApi.Data.DTOs;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    public string? Username { get; set; }

    [Required]
    public string Password { get; set; } = string.Empty;
}

public class LoginDTO : LoginRequestDto;
