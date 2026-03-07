namespace StancaBankApi.Core.Interfaces;

public interface ICustomerService
{
    string? Login(LoginDTO dto);
    void ChangeOwnPassword(int customerId, ChangePasswordDto dto);
    CustomerDTO? GetById(int id);
    List<CustomerDTO> GetAll();
}
