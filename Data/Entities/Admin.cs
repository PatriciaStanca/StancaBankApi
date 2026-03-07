namespace StancaBankApi.Data.Entities;

public class Admin
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;
    [MaxLength(200)]
    public string PasswordHash { get; set; } = string.Empty;
}
