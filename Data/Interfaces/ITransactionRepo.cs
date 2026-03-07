namespace StancaBankApi.Data.Interfaces;

public interface ITransactionRepo
{
    List<Transaction> GetByAccountId(int accountId);
    Transaction Add(Transaction transaction);
    bool HasForAccount(int accountId);
    void DeleteByAccountIds(List<int> accountIds);
}
