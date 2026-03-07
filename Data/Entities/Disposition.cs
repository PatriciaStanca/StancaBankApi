namespace StancaBankApi.Data.Entities;

[Table("Dispositions")]
public class Disposition
{
    [Key]
    [Column("DispositionId")]
    public int Id { get; set; }

    [Column("CustomerId")]
    public int CustomerId { get; set; }

    [Column("AccountId")]
    public int AccountId { get; set; }

    [MaxLength(20)]
    [Column("Type")]
    public string Role { get; set; } = "OWNER";
}
