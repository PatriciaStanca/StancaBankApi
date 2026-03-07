namespace StancaBankApi.Data.Entities;

[Table("Cards")]
public class Card
{
    [Key]
    [Column("CardId")]
    public int Id { get; set; }

    [Column("DispositionId")]
    public int DispositionId { get; set; }

    [MaxLength(50)]
    [Column("Type")]
    public string Type { get; set; } = string.Empty;

    [Column("Issued")]
    public DateTime Issued { get; set; }

    [MaxLength(50)]
    [Column("CCType")]
    public string CCType { get; set; } = string.Empty;

    [MaxLength(50)]
    [Column("CCNumber")]
    public string CCNumber { get; set; } = string.Empty;

    [MaxLength(10)]
    [Column("CVV2")]
    public string CVV2 { get; set; } = string.Empty;

    [Column("ExpM")]
    public int ExpM { get; set; }

    [Column("ExpY")]
    public int ExpY { get; set; }
}
