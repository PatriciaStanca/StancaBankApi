namespace StancaBankApi.Core.Interfaces;

public interface IAdminService
{
    string? Login(AdminLoginDto dto);
    void ChangeOwnPassword(string username, ChangePasswordDto dto);
    CustomerDTO CreateCustomer(CreateCustomerDTO dto);
    CustomerDTO UpdateCustomer(int customerId, UpdateCustomerDTO dto);
    void DeleteCustomer(int customerId);
    LoanDTO CreateLoan(CreateLoanDTO dto);
}
