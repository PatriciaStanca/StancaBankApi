namespace StancaBankApi.Data.Entities;

[Table("AccountTypes")]
public class AccountType
{
    [Key]
    [Column("AccountTypeId")]
    public int Id { get; set; }

    [MaxLength(50)]
    [Column("TypeName")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    [Column("Description")]
    public string? Description { get; set; }
}
