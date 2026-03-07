namespace StancaBankApi.Data.Entities;

[Table("Transactions")]
public class Transaction
{
    [Key]
    [Column("TransactionId")]
    public int Id { get; set; }

    [Column("AccountId")]
    public int AccountId { get; set; }

    [Column("Date")]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [MaxLength(50)]
    [Column("Type")]
    public string Type { get; set; } = "Transfer";

    [MaxLength(50)]
    [Column("Operation")]
    public string Operation { get; set; } = "Debit";

    [Column("Amount")]
    public decimal Amount { get; set; }

    [Column("Balance")]
    public decimal Balance { get; set; }

    [MaxLength(50)]
    [Column("Symbol")]
    public string? Symbol { get; set; }

    [MaxLength(50)]
    [Column("Bank")]
    public string? Bank { get; set; }

    [MaxLength(50)]
    [Column("Account")]
    public string? CounterpartyAccount { get; set; }
}
