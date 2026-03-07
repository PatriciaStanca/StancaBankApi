namespace StancaBankApi.Data.Interfaces;

public interface IAccountRepo
{
    Account? GetById(int id);
    Account? GetByAccountNumber(string accountNumber);
    List<Account> GetByCustomerId(int customerId);
    bool IsOwnedByCustomer(int accountId, int customerId);
    Account Add(Account account);
    void Update(Account account);
    void Delete(int id);
    void DeleteByCustomerId(int customerId);
}
