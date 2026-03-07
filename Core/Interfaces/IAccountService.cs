namespace StancaBankApi.Core.Interfaces;

public interface IAccountService
{
    List<AccountDTO> GetCustomerAccounts(int customerId);
    AccountDTO CreateAccountForCustomer(int customerId, int accountTypeId);
    AccountDTO UpdateAccountType(int customerId, int accountId, int accountTypeId);
    void DeleteAccount(int customerId, int accountId);
}
