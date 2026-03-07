namespace StancaBankApi.Data.Repos;

public class CustomerRepo(BankAppDataContext db) : ICustomerRepo
{
    public Customer? GetById(int id) => db.Customers.FirstOrDefault(c => c.Id == id);

    public Customer? GetByEmail(string emailAddress) =>
        db.Customers.FirstOrDefault(c => c.EmailAddress != null && c.EmailAddress.ToLower() == emailAddress.ToLower());

    public List<Customer> GetAll() => db.Customers.ToList();

    public Customer Add(Customer customer)
    {
        db.Customers.Add(customer);
        db.SaveChanges();
        return customer;
    }

    public void Update(Customer customer)
    {
        var existing = GetById(customer.Id) ?? throw new InvalidOperationException("Customer not found");
        existing.GivenName = customer.GivenName;
        existing.Surname = customer.Surname;
        existing.Gender = customer.Gender;
        existing.StreetAddress = customer.StreetAddress;
        existing.City = customer.City;
        existing.ZipCode = customer.ZipCode;
        existing.Country = customer.Country;
        existing.CountryCode = customer.CountryCode;
        existing.Birthday = customer.Birthday;
        existing.TelephoneCountryCode = customer.TelephoneCountryCode;
        existing.TelephoneNumber = customer.TelephoneNumber;
        existing.EmailAddress = customer.EmailAddress;
        db.SaveChanges();
    }

    public void Delete(int id)
    {
        var customer = GetById(id) ?? throw new InvalidOperationException("Customer not found");
        db.Customers.Remove(customer);
        db.SaveChanges();
    }
}
