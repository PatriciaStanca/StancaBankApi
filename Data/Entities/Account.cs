namespace StancaBankApi.Data.Entities;

[Table("Accounts")]
public class Account
{
    [Key]
    [Column("AccountId")]
    public int Id { get; set; }

    [MaxLength(50)]
    [Column("Frequency")]
    public string Frequency { get; set; } = "Monthly";

    [Column("Created")]
    public DateTime Created { get; set; } = DateTime.UtcNow;

    [Column("AccountTypesId")]
    public int? AccountTypeId { get; set; }

    [Column("Balance")]
    public decimal Balance { get; set; }
}
