namespace StancaBankApi.Data.DTOs;

public class CustomerDTO
{
    public int Id { get; set; }
    public string EmailAddress { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
}
