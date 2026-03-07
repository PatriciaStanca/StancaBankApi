namespace StancaBankApi.Data.DTOs;

public class UpdateAuthUserDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = string.Empty;
}
