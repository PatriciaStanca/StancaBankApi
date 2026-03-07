namespace StancaBankApi.Data.Entities;

[Table("AuthUsers")]
public class AuthUser
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("CustomerId")]
    public int? CustomerId { get; set; }

    [Required]
    [MaxLength(450)]
    [Column("Username")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [Column("PasswordHash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [Column("Role")]
    public string Role { get; set; } = string.Empty;

    [Column("RequirePasswordChange")]
    public bool RequirePasswordChange { get; set; }
}
