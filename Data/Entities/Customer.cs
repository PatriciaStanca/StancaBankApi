namespace StancaBankApi.Data.Entities;

[Table("Customers")]
public class Customer
{
    [Key]
    [Column("CustomerId")]
    public int Id { get; set; }

    [MaxLength(6)]
    [Column("Gender")]
    public string Gender { get; set; } = "U";

    [MaxLength(100)]
    [Column("Givenname")]
    public string GivenName { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column("Surname")]
    public string Surname { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column("Streetaddress")]
    public string StreetAddress { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column("City")]
    public string City { get; set; } = string.Empty;

    [MaxLength(15)]
    [Column("Zipcode")]
    public string ZipCode { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column("Country")]
    public string Country { get; set; } = string.Empty;

    [MaxLength(2)]
    [Column("CountryCode")]
    public string CountryCode { get; set; } = "SE";

    [Column("Birthday")]
    public DateTime? Birthday { get; set; }

    [MaxLength(10)]
    [Column("Telephonecountrycode")]
    public string? TelephoneCountryCode { get; set; }

    [MaxLength(25)]
    [Column("Telephonenumber")]
    public string? TelephoneNumber { get; set; }

    [MaxLength(100)]
    [Column("Emailaddress")]
    public string? EmailAddress { get; set; }
}
