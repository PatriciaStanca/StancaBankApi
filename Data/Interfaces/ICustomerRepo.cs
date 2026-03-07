namespace StancaBankApi.Data.Interfaces;

public interface ICustomerRepo
{
    Customer? GetById(int id);
    Customer? GetByEmail(string emailAddress);
    List<Customer> GetAll();
    Customer Add(Customer customer);
    void Update(Customer customer);
    void Delete(int id);
}
