namespace StancaBankApi.Data.Entities;

[Table("Loans")]
public class Loan
{
    [Key]
    [Column("LoanId")]
    public int Id { get; set; }

    [Column("AccountId")]
    public int AccountId { get; set; }

    [Column("Date")]
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [Column("Amount")]
    public decimal Amount { get; set; }

    [Column("Duration")]
    public int Duration { get; set; }

    [Column("Payments")]
    public decimal Payments { get; set; }

    [Column("Status")]
    public string Status { get; set; } = "Active";

    [Column("DurationMonths")]
    public int DurationMonths { get; set; }

    [Column("InterestRate")]
    public double InterestRate { get; set; }
}
